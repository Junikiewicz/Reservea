using System.Collections.Generic;

namespace Reservea.Microservices.Resources.Dtos.Requests
{
    public class UpdateResourceTypeRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ResourceTypeAttributeRequest> ResourceTypeAttributes { get; set; }
    }

    public class ResourceTypeAttributeRequest
    {
        public int AttributeId { get; set; }
    }
}
