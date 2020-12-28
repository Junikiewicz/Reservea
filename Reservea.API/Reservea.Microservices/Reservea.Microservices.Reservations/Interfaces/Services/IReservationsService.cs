using Reservea.Microservices.Reservations.Dtos.Requests;
using Reservea.Microservices.Reservations.Dtos.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.Reservations.Interfaces.Services
{
    public interface IReservationsService
    {
        Task<IEnumerable<ReservationForTimelineResponse>> GetResourceTypeReservationsAsync(int resourceTypeId, CancellationToken cancellationToken);
        Task CreateReservationAsync(IEnumerable<NewReservationRequest> reservations, int userId, CancellationToken cancellationToken);
        Task<IEnumerable<ReservationForListResponse>> GetReservationsForListAsync(CancellationToken cancellationToken);
    }
}
