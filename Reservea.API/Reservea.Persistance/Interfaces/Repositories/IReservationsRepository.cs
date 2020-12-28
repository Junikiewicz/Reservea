using Reservea.Persistance.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Persistance.Interfaces.Repositories
{
    public interface IReservationsRepository : IGenericRepository<Reservation>
    {
        Task<bool> CheckIfCollidingReservationExists(Reservation reservation, CancellationToken cancellationToken);
    }
}
