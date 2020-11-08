using Reservea.Microservices.Resources.Dtos.Requests;
using Reservea.Microservices.Resources.Dtos.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.Resources.Interfaces.Services
{
    public interface IResourcesService
    {
        Task<IEnumerable<ResourceForListResponse>> GetAllResourcesForListAsync(CancellationToken cancellationToken);
        Task<ResourceForDetailedResponse> GetResourceDetailsByIdAsync(int resourceId, CancellationToken cancellationToken);
        Task<AddResourceResponse> AddResourceAsync(AddResourceRequest request, CancellationToken cancellationToken);
        Task UpdateResourceAttributesAsync(int resourceId, UpdateResourceAttributesRequest request, CancellationToken cancellationToken);
    }
}
