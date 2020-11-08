using System.Collections.Generic;

namespace Reservea.Persistance.Models
{
    public class ResourceType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ResourceTypeAttribute> ResourceTypeAttributes { get; set; }
        public virtual ICollection<Resource> Resources { get; set; }
    }
}
