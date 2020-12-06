using AutoMapper;
using Reservea.Persistance.Interfaces.Repositories;
using Reservea.Persistance.Models;

namespace Reservea.Persistance.Repositories
{
    class ResourceTypeAttributesRepository : GenericRepository<ResourceTypeAttribute>, IResourceTypeAttributesRepository
    {
        public ResourceTypeAttributesRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}
