using Microsoft.AspNetCore.Mvc;
using Reservea.Microservices.Users.Interfaces.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rolesService;
        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRolesAsync(CancellationToken cancellationToken)
        {
            return Ok(await _rolesService.GetRolesAsync(cancellationToken));
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserRolesAsync(int userId, CancellationToken cancellationToken)
        {
            return Ok(await _rolesService.GetUserRolesAsync(userId, cancellationToken));
        }

        [HttpPatch("{userId}")]
        public async Task UpdateUserRolesAsync(int userId, IEnumerable<string> newUserRoles, CancellationToken cancellationToken)
        {
            await _rolesService.UpdateUserRoles(userId, newUserRoles);
        }

        [HttpPost]
        public async Task AddRole(string roleName, CancellationToken cancellationToken)
        {
            await _rolesService.AddRoleAsync(roleName);
        }
    }
}
