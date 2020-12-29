using Microsoft.AspNetCore.Mvc;
using Reservea.Microservices.Reservations.Dtos.Requests;
using Reservea.Microservices.Reservations.Interfaces.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.Reservations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationsService _reservationsService;
        public ReservationsController(IReservationsService reservationsService)
        {
            _reservationsService = reservationsService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservationAsync(IEnumerable<NewReservationRequest> reservations, CancellationToken cancellationToken)
        {
            var userId = 1; //temp

            await _reservationsService.CreateReservationAsync(reservations, userId, cancellationToken);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetResourceTypeReservations([FromQuery]int resourceTypeId, CancellationToken cancellationToken)
        {
            return Ok(await _reservationsService.GetResourceTypeReservationsAsync(resourceTypeId,cancellationToken));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllReservations(CancellationToken cancellationToken)
        {
            return Ok(await _reservationsService.GetReservationsForListAsync(cancellationToken));
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserReservations(CancellationToken cancellationToken)
        {
            var userId = 1; //temp

            return Ok(await _reservationsService.GetUserReservations(userId, cancellationToken));
        }
    }
}
