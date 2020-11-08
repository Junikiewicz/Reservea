using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Reservea.Persistance.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Persistance.Repositories
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected DataContext _context;
        protected IMapper _mapper;

        public GenericRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<TEntity>().ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(CancellationToken cancellationToken)
        {
            var query = _mapper.ProjectTo<TResult>(_context.Set<TEntity>());

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<TEntity> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken)
        {
            var lambda = CreateFindByPrimaryKeyLambda<TId, TEntity>(id);

            var query = _context.Set<TEntity>();

            return await query.SingleAsync(lambda, cancellationToken);
        }

        public async Task<TResult> GetByIdAsync<TId, TResult>(TId id, CancellationToken cancellationToken)
        {
            var lambda = CreateFindByPrimaryKeyLambda<TId, TResult>(id);

            var query = _mapper.ProjectTo<TResult>(_context.Set<TEntity>());

            return await query.SingleAsync(lambda, cancellationToken);
        }

        public void Add(TEntity entity)
        {
            _context.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entity)
        {
            _context.AddRange(entity);
        }

        #region Private helpers

        private Expression<Func<TResult, bool>> CreateFindByPrimaryKeyLambda<TId, TResult>(TId id)
        {
            var primaryKeyProperties = _context.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties;

            if(primaryKeyProperties.Count <= 0)
            {
                throw new Exception();
            }
            else if (primaryKeyProperties.Count == 1)
            {
                var pkPropertyName = primaryKeyProperties.Select(x => x.Name).Single();
                var propertyInfo = typeof(TResult).GetProperty(pkPropertyName);

                var parameter = Expression.Parameter(typeof(TResult), "x");
                var member = Expression.MakeMemberAccess(parameter, propertyInfo);
                var constant = Expression.Constant(id, id.GetType());
                var equation = Expression.Equal(member, constant);

                return Expression.Lambda<Func<TResult, bool>>(equation, parameter);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        #endregion
    }
}
