using Microsoft.AspNetCore.Mvc;
using Reservea.Microservices.Users.Dtos.Requests;
using Reservea.Microservices.Users.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            return Ok(await _usersService.GetUsersAsync(cancellationToken));
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserDetailsAsync(int userId, CancellationToken cancellationToken)
        {
            return Ok(await _usersService.GetUserDetailsAsync(userId, cancellationToken));
        }

        [HttpPatch("{userId}")]
        public async Task UpdateUserAsync(int userId, UpdateUserRequest request, CancellationToken cancellationToken)
        {
            await _usersService.UpdateUserAsync(userId, request, cancellationToken);
        }
    }
}
