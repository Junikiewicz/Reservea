using Reservea.Persistance.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Persistance.Interfaces.Repositories
{
    public interface IResourceTypesRepository: IGenericRepository<ResourceType>
    {
        Task<IEnumerable<int>> GetResourceTypeAttributesIds(int resourceTypeId, CancellationToken cancellationToken);
        Task<IEnumerable<Attribute>> GetResourceTypeAttributes(int resourceTypeId, CancellationToken cancellationToken);
    }
}
