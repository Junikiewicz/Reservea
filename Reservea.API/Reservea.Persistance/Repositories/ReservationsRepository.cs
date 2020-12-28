using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Reservea.Persistance.Interfaces.Repositories;
using Reservea.Persistance.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Persistance.Repositories
{
    public class ReservationsRepository : GenericRepository<Reservation>, IReservationsRepository
    {
        public ReservationsRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public async Task<bool> CheckIfCollidingReservationExists(Reservation reservation, CancellationToken cancellationToken)
        {
            return await _context.Reservations.AnyAsync(x => x.Start < reservation.End && x.End > reservation.Start, cancellationToken);
        }
    }
}