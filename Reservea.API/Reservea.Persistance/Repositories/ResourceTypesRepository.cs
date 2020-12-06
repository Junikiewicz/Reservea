using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Reservea.Persistance.Interfaces.Repositories;
using Reservea.Persistance.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Persistance.Repositories
{
    public class ResourceTypesRepository : GenericRepository<ResourceType>, IResourceTypesRepository
    {
        public ResourceTypesRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<IEnumerable<int>> GetResourceTypeAttributesIds(int resourceTypeId, CancellationToken cancellationToken)
        {
            var query = _context.ResourceTypes
                .Where(x => x.Id == resourceTypeId)
                    .Select(rt => rt.ResourceTypeAttributes
                        .Select(rta => rta.AttributeId));

            return await query.SingleAsync(cancellationToken);
        }
        public async Task<IEnumerable<Attribute>> GetResourceTypeAttributes(int resourceTypeId, CancellationToken cancellationToken)
        {
            var query = _context.ResourceTypes
                .Where(x => x.Id == resourceTypeId)
                    .Select(rt => rt.ResourceTypeAttributes
                        .Select(rta => new Attribute { Id =  rta.AttributeId, Name = rta.Attribute.Name }));

            return await query.SingleAsync(cancellationToken);
        }

    }
}