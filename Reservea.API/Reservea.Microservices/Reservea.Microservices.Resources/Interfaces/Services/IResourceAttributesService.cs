using Reservea.Microservices.Resources.Dtos.Requests;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.Resources.Interfaces.Services
{
    public interface IResourceAttributesService
    {
        Task UpdateResourceAttributesAsync(int resourceId, UpdateResourceAttributesRequest request, CancellationToken cancellationToken);
    }
}
