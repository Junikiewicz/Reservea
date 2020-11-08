using System.Collections.Generic;

namespace Reservea.Microservices.Resources.Dtos.Responses
{
    public class ResourceForDetailedResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerHour { get; set; }
        public int ResourceStatusId { get; set; }
        public int ResourceTypeId { get; set; }

        public virtual ICollection<ResourceAttributeForDetailedResourceResponse> ResourceAttributes { get; set; }
    }
    public class ResourceAttributeForDetailedResourceResponse
    {
        public int AttributeId { get; set; }
        public int ResourceId { get; set; }
        public string Value { get; set; }
    }
}
