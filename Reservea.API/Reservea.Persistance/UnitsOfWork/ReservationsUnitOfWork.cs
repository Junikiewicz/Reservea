using AutoMapper;
using Reservea.Persistance.Interfaces.Repositories;
using Reservea.Persistance.Interfaces.UnitsOfWork;
using Reservea.Persistance.Repositories;

namespace Reservea.Persistance.UnitsOfWork
{
    public class ReservationsUnitOfWork : BasicUnitOfWork, IReservationsUnitOfWork
    {
        public IReservationsRepository ReservationsRepository
        {
            get
            {
                if (_reservationsRepository is null)
                {
                    _reservationsRepository = new ReservationsRepository(_context, _mapper);
                }

                return _reservationsRepository;
            }
        }

        private IReservationsRepository _reservationsRepository;

        public ReservationsUnitOfWork(DataContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}
