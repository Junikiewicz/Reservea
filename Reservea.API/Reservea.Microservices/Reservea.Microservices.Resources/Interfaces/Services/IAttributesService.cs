using Reservea.Microservices.Resources.Dtos.Requests;
using Reservea.Microservices.Resources.Dtos.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.Resources.Interfaces.Services
{
    public interface IAttributesService
    {
        Task<IEnumerable<AttributeForListResponse>> GetAllAttributesForList(CancellationToken cancellationToken);
        Task<AddAttributeResponse> AddAttribute(AddAttributeRequest request, CancellationToken cancellationToken);
        Task EditAttribute(int id, EditAttributeRequest request, CancellationToken cancellationToken);
        Task DeleteAttribute(int id, CancellationToken cancellationToken);
    }
}
