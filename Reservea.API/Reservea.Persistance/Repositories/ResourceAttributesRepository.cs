using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Reservea.Persistance.Interfaces.Repositories;
using Reservea.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Persistance.Repositories
{
    public class ResourceAttributesRepository : GenericRepository<ResourceAttribute>, IResourceAttributesRepository
    {
        public ResourceAttributesRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public async Task RemoveByIdAsync<TId>(ResourceAttributePrimaryKey primaryKey, CancellationToken cancellationToken)
        {
            var entity = await GetByIdAsync(primaryKey, cancellationToken);

            Remove(entity);
        }

        public async Task RemoveByListOfIdsAsync(IEnumerable<ResourceAttributePrimaryKey> ids, CancellationToken cancellationToken)
        {
            var entity = await GetByListOfIdsAsync(ids, cancellationToken);

            RemoveRange(entity);
        }
        public async Task<ResourceAttribute> GetByIdAsync(ResourceAttributePrimaryKey primaryKey, CancellationToken cancellationToken)
        {
            var query = _context.ResourceAttributes.Where(x => x.ResourceId == primaryKey.ResourceId && x.AttributeId == primaryKey.AttributeId);

            return await query.SingleAsync(cancellationToken);
        }

        public async Task<TResult> GetByIdAsync<TResult>(ResourceAttributePrimaryKey primaryKey, CancellationToken cancellationToken)
        {
            var query = _context.ResourceAttributes.Where(x => x.ResourceId == primaryKey.ResourceId && x.AttributeId == primaryKey.AttributeId);

            var mappedQuery = _mapper.ProjectTo<TResult>(query);

            return await mappedQuery.SingleAsync(cancellationToken);
        }

        public async Task<IEnumerable<ResourceAttribute>> GetByListOfIdsAsync(IEnumerable<ResourceAttributePrimaryKey> ids, CancellationToken cancellationToken)
        {
            var parameter = Expression.Parameter(typeof(ResourceAttribute));

            var body = ids
                .Select(b => Expression.AndAlso(
                    Expression.Equal(Expression.Property(parameter, "ResourceId"), Expression.Constant(b.ResourceId)),
                    Expression.Equal(Expression.Property(parameter, "AttributeId"), Expression.Constant(b.AttributeId))))
                .Aggregate(Expression.OrElse);

            var predicate = Expression.Lambda<Func<ResourceAttribute, bool>>(body, parameter);

            var query = _context.ResourceAttributes.Where(predicate);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TResult>> GetByListOfIdsAsync<TResult>(IEnumerable<ResourceAttributePrimaryKey> ids, CancellationToken cancellationToken)
        {
            var parameter = Expression.Parameter(typeof(ResourceAttribute));

            var body = ids
                .Select(b => Expression.AndAlso(
                    Expression.Equal(Expression.Property(parameter, "ResourceId"), Expression.Constant(b.ResourceId)),
                    Expression.Equal(Expression.Property(parameter, "AttributeId"), Expression.Constant(b.AttributeId))))
                .Aggregate(Expression.OrElse);

            var predicate = Expression.Lambda<Func<ResourceAttribute, bool>>(body, parameter);

            var query = _context.ResourceAttributes.Where(predicate);

            var mappedQuery = _mapper.ProjectTo<TResult>(query);

            return await mappedQuery.ToListAsync(cancellationToken);
        }
    }
}
