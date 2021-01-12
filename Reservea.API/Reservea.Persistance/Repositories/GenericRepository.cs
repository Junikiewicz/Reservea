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

        public async Task<int> CountAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<TEntity>().CountAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            return await _context.Set<TEntity>().ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(CancellationToken cancellationToken, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            var query = _mapper.ProjectTo<TResult>(_context.Set<TEntity>());

            return await query.ToListAsync(cancellationToken);
        }

        public async virtual Task<IEnumerable<TResult>> GetAsync<TResult>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            var query = _context.Set<TEntity>().Where(predicate);

            var mappedQuery = _mapper.ProjectTo<TResult>(query);

            return await mappedQuery.ToListAsync(cancellationToken);
        }

        public async virtual Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            var query = _context.Set<TEntity>().Where(predicate);

            if (include != null)
            {
                query = include.Invoke(query);
            }

            return await query.ToListAsync(cancellationToken);
        }

        public async virtual Task<TResult> GetSingleAsync<TResult>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            var query = _context.Set<TEntity>().Where(predicate);

            var mappedQuery = _mapper.ProjectTo<TResult>(query);

            return await mappedQuery.SingleAsync(cancellationToken);
        }

        public async virtual Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            var query = _context.Set<TEntity>().Where(predicate);

            if (include != null)
            {
                query = include.Invoke(query);
            }

            return await query.SingleAsync(cancellationToken);
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
    }
}