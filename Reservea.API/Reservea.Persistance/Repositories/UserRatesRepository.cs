using AutoMapper;
using Reservea.Persistance.Interfaces.Repositories;
using Reservea.Persistance.Models;

namespace Reservea.Persistance.Repositories
{
    public class UserRatesRepository : GenericRepository<UserRate>, IUserRatesRepository
    {
        public UserRatesRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}
