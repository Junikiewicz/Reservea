using AutoMapper;
using Reservea.Persistance.Interfaces.Repositories;
using Reservea.Persistance.Models;

namespace Reservea.Persistance.Repositories
{
    public class AttributesRepository : GenericRepository<Attribute>, IAttributesRepository
    {
        public AttributesRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}
