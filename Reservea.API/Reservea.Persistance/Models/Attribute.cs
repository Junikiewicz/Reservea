using System.Collections.Generic;

namespace Reservea.Persistance.Models
{
    public class Attribute
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ResourceAttribute> ResourceAttributes { get; set; }
        public ICollection<ResourceTypeAttribute> ResourceTypeAttributes { get; set; }
    }
}
