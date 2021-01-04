using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reservea.Microservices.Users.Interfaces.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.Users.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rolesService;
        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpGet]
        public async Task<IActionResult> GetAllRolesAsync(CancellationToken cancellationToken)
        {
            return Ok(await _rolesService.GetRolesAsync(cancellationToken));
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserRolesAsync(int userId, CancellationToken cancellationToken)
        {
            return Ok(await _rolesService.GetUserRolesAsync(userId, cancellationToken));
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpPatch("{userId}")]
        public async Task UpdateUserRolesAsync(int userId, IEnumerable<string> newUserRoles, CancellationToken cancellationToken)
        {
            await _rolesService.UpdateUserRoles(userId, newUserRoles);
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpPost]
        public async Task AddRole(string roleName, CancellationToken cancellationToken)
        {
            await _rolesService.AddRoleAsync(roleName);
        }
    }
}
