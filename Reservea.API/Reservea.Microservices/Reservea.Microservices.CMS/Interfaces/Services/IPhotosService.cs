using Reservea.Microservices.CMS.Dtos.Requests;
using Reservea.Microservices.CMS.Dtos.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.CMS.Interfaces.Services
{
    public interface IPhotosService
    {
        Task<IEnumerable<PhotoResponse>> GetPhotos(CancellationToken cancellationToken);
        Task<PhotoResponse> AddPhoto(AddPhotoRequest request, CancellationToken cancellationToken);
        Task<PhotoResponse> GetPhoto(int id, CancellationToken cancellationToken);
        Task DeletePhoto(int id, CancellationToken cancellationToken);
    }
}
