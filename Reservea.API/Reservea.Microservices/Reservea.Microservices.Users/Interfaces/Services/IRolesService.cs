using Reservea.Microservices.Users.Dtos.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.Users.Interfaces.Services
{
    public interface IRolesService
    {
        Task<IEnumerable<RoleResponse>> GetRolesAsync(CancellationToken cancellationToken);
        Task<IEnumerable<RoleResponse>> GetUserRolesAsync(int userId, CancellationToken cancellationToken);
        Task UpdateUserRoles(int userId, IEnumerable<string> rolesToAdd);
        Task AddRoleAsync(string roleName);
    }
}
