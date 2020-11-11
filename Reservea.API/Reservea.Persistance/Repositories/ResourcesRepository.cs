using AutoMapper;
using Reservea.Persistance.Interfaces.Repositories;
using Reservea.Persistance.Models;

namespace Reservea.Persistance.Repositories
{
    public class ResourcesRepository : GenericRepository<Resource>, IResourcesRepository
    {
        public ResourcesRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
