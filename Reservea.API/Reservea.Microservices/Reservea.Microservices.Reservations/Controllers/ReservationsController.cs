using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reservea.Microservices.Reservations.Dtos.Requests;
using Reservea.Microservices.Reservations.Interfaces.Services;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.Reservations.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationsService _reservationsService;
        public ReservationsController(IReservationsService reservationsService)
        {
            _reservationsService = reservationsService;
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> CreateReservationAsync(IEnumerable<NewReservationRequest> reservations, CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            await _reservationsService.CreateReservationAsync(reservations, userId, cancellationToken);

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetResourceTypeReservations([FromQuery]int resourceTypeId, CancellationToken cancellationToken)
        {
            return Ok(await _reservationsService.GetResourceTypeReservationsAsync(resourceTypeId,cancellationToken));
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllReservations(CancellationToken cancellationToken)
        {
            return Ok(await _reservationsService.GetReservationsForListAsync(cancellationToken));
        }

        [Authorize(Roles = "Customer,Admin,Employee")]
        [HttpGet("user")]
        public async Task<IActionResult> GetUserReservations(CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return Ok(await _reservationsService.GetUserReservations(userId, cancellationToken));
        }
    }
}
