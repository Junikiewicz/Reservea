using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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

        public async virtual Task<TEntity> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken)
        {
            var lambda = CreateFindByPrimaryKeyLambda(id);

            var query = _context.Set<TEntity>();

            return await query.SingleAsync(lambda, cancellationToken);
        }

        public async virtual Task<TResult> GetByIdAsync<TId, TResult>(TId id, CancellationToken cancellationToken)
        {
            var lambda = CreateFindByPrimaryKeyLambda(id);

            var query = _context.Set<TEntity>().Where(lambda);

            var mappedQuery = _mapper.ProjectTo<TResult>(query);

            return await mappedQuery.SingleAsync(cancellationToken);
        }

        public async virtual Task<TEntity> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            var lambda = CreateFindByPrimaryKeyLambda(id);
            var query = _context.Set<TEntity>().AsQueryable();

            if (include != null)
            {
                query = include.Invoke(query);
            }

            return await query.SingleAsync(lambda, cancellationToken);
        }

        public async virtual Task<IEnumerable<TEntity>> GetByListOfIdsAsync<TId>(IEnumerable<TId> ids, CancellationToken cancellationToken)
        {
            var lambda = CreateFindByPrimaryKeyLambda(ids);

            var query = _context.Set<TEntity>().Where(lambda);

            return await query.ToListAsync(cancellationToken);
        }

        public async virtual Task<IEnumerable<TResult>> GetByListOfIdsAsync<TId, TResult>(IEnumerable<TId> ids, CancellationToken cancellationToken)
        {
            var lambda = CreateFindByPrimaryKeyLambda(ids);

            var query = _mapper.ProjectTo<TResult>(_context.Set<TEntity>());

            return await query.ToListAsync(cancellationToken);
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            _context.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entity)
        {
            _context.Set<TEntity>().RemoveRange(entity);
        }

        public virtual async Task RemoveByIdAsync<TId>(TId id, CancellationToken cancellationToken)
        {
            var entity = await GetByIdAsync(id, cancellationToken);

            Remove(entity);
        }

        public virtual async Task RemoveByListOfIdsAsync<TId>(IEnumerable<TId> ids, CancellationToken cancellationToken)
        {
            var entity = await GetByListOfIdsAsync(ids, cancellationToken);

            RemoveRange(entity);
        }

        #region Private helpers
        protected Expression<Func<TEntity, bool>> CreateFindByPrimaryKeyLambda<TId>(IEnumerable<TId> ids)
        {
            var primaryKeyProperties = _context.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties;

            if (primaryKeyProperties.Count != 1) throw new Exception();

            var pkPropertyName = primaryKeyProperties.Select(x => x.Name).Single();
            var propertyInfo = typeof(TEntity).GetProperty(pkPropertyName);
            var method = ids.GetType().GetMethod("Contains");

            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var member = Expression.MakeMemberAccess(parameter, propertyInfo);
            var constant = Expression.Constant(ids, ids.GetType());
            var call = Expression.Call(constant, method, member);

            return Expression.Lambda<Func<TEntity, bool>>(call, parameter);
        }

        protected Expression<Func<TEntity, bool>> CreateFindByPrimaryKeyLambda<TId>(TId id)
        {
            var primaryKeyProperties = _context.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties;

            if (primaryKeyProperties.Count != 1) throw new Exception();

            var pkPropertyName = primaryKeyProperties.Select(x => x.Name).Single();
            var propertyInfo = typeof(TEntity).GetProperty(pkPropertyName);

            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var member = Expression.MakeMemberAccess(parameter, propertyInfo);
            var constant = Expression.Constant(id, id.GetType());
            var equation = Expression.Equal(member, constant);

            return Expression.Lambda<Func<TEntity, bool>>(equation, parameter);
        }
        #endregion
    }
}