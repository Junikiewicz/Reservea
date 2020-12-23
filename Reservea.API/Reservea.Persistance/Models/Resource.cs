using System.Collections.Generic;

namespace Reservea.Persistance.Models
{
    public class Resource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerHour { get; set; }
        public int ResourceStatusId { get; set; }
        public int ResourceTypeId { get; set; }

        public virtual ResourceStatus ResourceStatus { get; set; }
        public virtual ResourceType ResourceType { get; set; }
        public virtual ICollection<ResourceAttribute> ResourceAttributes { get; set; }
        public virtual ICollection<ResourceAvailability> ResourceAvailabilities { get; set; }
    }
}
