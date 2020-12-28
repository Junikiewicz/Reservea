using Reservea.Persistance.Interfaces.Repositories;

namespace Reservea.Persistance.Interfaces.UnitsOfWork
{
    public interface IReservationsUnitOfWork : IBasicUnitOfWork
    {
        IReservationsRepository ReservationsRepository { get; }
    }
}
