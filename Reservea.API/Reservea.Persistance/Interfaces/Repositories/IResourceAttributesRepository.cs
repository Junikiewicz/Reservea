using Reservea.Persistance.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Persistance.Interfaces.Repositories
{
    public interface IResourceAttributesRepository : IGenericRepository<ResourceAttribute>
    {
        Task DeleteByIdAsync<TId>(ResourceAttributePrimaryKey primaryKey, CancellationToken cancellationToken);
        Task DeleteByListOfIdsAsync(IEnumerable<ResourceAttributePrimaryKey> primaryKeys, CancellationToken cancellationToken);
        Task<ResourceAttribute> GetByIdAsync(ResourceAttributePrimaryKey primaryKey, CancellationToken cancellationToken);
        Task<TResult> GetByIdAsync<TResult>(ResourceAttributePrimaryKey primaryKey, CancellationToken cancellationToken);
        Task<IEnumerable<ResourceAttribute>> GetByListOfIdsAsync(IEnumerable<ResourceAttributePrimaryKey> primaryKeys, CancellationToken cancellationToken);
        Task<IEnumerable<TResult>> GetByListOfIdsAsync<TResult>(IEnumerable<ResourceAttributePrimaryKey> primaryKeys, CancellationToken cancellationToken);
    }

    public class ResourceAttributePrimaryKey
    {
        public int ResourceId { get; set; }
        public int AttributeId { get; set; }
    }
}