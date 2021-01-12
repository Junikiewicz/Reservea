using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reservea.Microservices.CMS.Dtos.Requests;
using Reservea.Microservices.CMS.Interfaces.Services;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.CMS.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserRatesController : ControllerBase
    {
        private readonly IUserRatesService _userRatesService;

        public UserRatesController(IUserRatesService userRatesService)
        {
            _userRatesService = userRatesService;
        }

        [HttpPost]
        public async Task<IActionResult> AddUserRateAsync(CreateUserRateRequest request, CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            await _userRatesService.AddUserRateAsync(request, userId, cancellationToken);

            return NoContent();
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpPatch]
        public async Task<IActionResult> UpdateUserRatesAsync(IEnumerable<UserRateUpdateRequest> ratesToUpdate, CancellationToken cancellationToken)
        {
            await _userRatesService.UpdateUserRatesAsync(ratesToUpdate, cancellationToken);

            return NoContent();
        }

        [Authorize(Roles = "Admin, Employee")]
        [HttpGet]
        public async Task<IActionResult> GetAllUserRates(CancellationToken cancellationToken)
        {
            return Ok(await _userRatesService.GetAllUserRatesAsync(cancellationToken));
        }

        [AllowAnonymous]
        [HttpGet("homepage")]
        public async Task<IActionResult> GetRatesForHomepage(CancellationToken cancellationToken)
        {
            return Ok(await _userRatesService.GetUserRatesForHomepageAsync(cancellationToken));
        }
    }
}
