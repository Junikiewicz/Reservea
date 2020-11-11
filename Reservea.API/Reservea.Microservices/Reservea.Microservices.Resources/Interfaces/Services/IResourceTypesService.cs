using Reservea.Microservices.Resources.Dtos.Requests;
using Reservea.Microservices.Resources.Dtos.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.Resources.Interfaces.Services
{
    public interface IResourceTypesService
    {
        Task<IEnumerable<ResourceTypeForListResponse>> GetAllResourceTypesForListAsync(CancellationToken cancellationToken);
        Task<ResourceTypeForDetailedResponse> GetResourceTypeDetailsAsync(int resourceTypeId, CancellationToken cancellationToken);
        Task UpdateResourceTypeAsync(int resourceTypeId, UpdateResourceTypeRequest request, CancellationToken cancellationToken);
        Task<AddResourceTypeResponse> AddResourceTypeAsync(AddResourceTypeRequest request, CancellationToken cancellationToken);
        Task DeleteResourceTypeAsync(int resourceTypeId, CancellationToken cancellationToken);
    }
}
