using Reservea.Microservices.CMS.Dtos.Requests;
using Reservea.Microservices.CMS.Dtos.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.CMS.Interfaces.Services
{
    public interface IUserRatesService
    {
        Task AddUserRateAsync(CreateUserRateRequest request, int userId, CancellationToken cancellationToken);
        Task<IEnumerable<UserRateForListResponse>> GetAllUserRatesAsync(CancellationToken cancellationToken);
        Task<IEnumerable<UserRateForHomePageResponse>> GetUserRatesForHomepageAsync(CancellationToken cancellationToken);
        Task UpdateUserRatesAsync(IEnumerable<UserRateUpdateRequest> request, CancellationToken cancellationToken);
    }
}
