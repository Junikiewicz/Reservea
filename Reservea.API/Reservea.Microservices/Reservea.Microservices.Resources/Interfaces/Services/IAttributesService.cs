using Reservea.Microservices.Resources.Dtos.Requests;
using Reservea.Microservices.Resources.Dtos.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.Resources.Interfaces.Services
{
    public interface IAttributesService
    {
        Task<IEnumerable<AttributeForListResponse>> GetAllAttributesForListAsync(CancellationToken cancellationToken);
        Task<AddAttributeResponse> AddAttributeAsync(AddAttributeRequest request, CancellationToken cancellationToken);
        Task UpdateAttributeAsync(int id, UpdateAttributeRequest request, CancellationToken cancellationToken);
        Task RemoveAttributeAsync(int id, CancellationToken cancellationToken);
    }
}
