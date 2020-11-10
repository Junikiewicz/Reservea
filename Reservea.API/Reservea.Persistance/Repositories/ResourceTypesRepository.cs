using AutoMapper;
using Reservea.Persistance.Interfaces.Repositories;
using Reservea.Persistance.Models;

namespace Reservea.Persistance.Repositories
{
    public class ResourceTypesRepository : GenericRepository<ResourceType>, IResourceTypesRepository
    {
        public ResourceTypesRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}