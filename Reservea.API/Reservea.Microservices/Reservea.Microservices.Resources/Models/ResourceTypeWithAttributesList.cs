using Reservea.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservea.Microservices.Resources.Models
{
    public class ResourceTypeWithAttributesList
    {
        public int ResourceTypeId { get; set; }
        public IEnumerable<ResourceTypeAttribute> ResourceTypeAttributes { get; set; }
    }
}
