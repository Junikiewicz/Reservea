using AutoMapper;
using Reservea.Persistance.Interfaces.Repositories;
using Reservea.Persistance.Models;

namespace Reservea.Persistance.Repositories
{
    public class ResourceAvailabilitiesRepository : GenericRepository<ResourceAvailability>, IResourceAvailabilitiesRepository
    {
        public ResourceAvailabilitiesRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}
