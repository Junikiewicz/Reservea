using AutoMapper;
using Reservea.Persistance.Interfaces.Repositories;
using Reservea.Persistance.Models;

namespace Reservea.Persistance.Repositories
{
    public class ResourceAttributesRepository : GenericRepository<ResourceAttribute>, IResourceAttributesRepository
    {
        public ResourceAttributesRepository(DataContext context, IMapper mapper) : base(context,mapper)
        {
        }
    }
}
