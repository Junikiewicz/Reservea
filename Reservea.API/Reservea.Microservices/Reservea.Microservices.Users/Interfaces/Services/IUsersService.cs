using Reservea.Microservices.Users.Dtos.Requests;
using Reservea.Microservices.Users.Dtos.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.Users.Interfaces.Services
{
    public interface IUsersService
    {
        Task<IEnumerable<UserForListResponse>> GetUsersAsync(CancellationToken cancellationToken);
        Task<UserForDetailedResponse> GetUserDetailsAsync(int userId, CancellationToken cancellationToken);
        Task UpdateUserAsync(int userId, UpdateUserRequest request, CancellationToken cancellationToken);
    }
}
