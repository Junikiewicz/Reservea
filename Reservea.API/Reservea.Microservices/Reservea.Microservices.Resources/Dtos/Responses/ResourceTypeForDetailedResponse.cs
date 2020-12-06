using System.Collections.Generic;

namespace Reservea.Microservices.Resources.Dtos.Responses
{
    public class ResourceTypeForDetailedResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ResourceTypeAttributeForDetailedResourceResponse> ResourceTypeAttributes { get; set; }
    }
}

public class ResourceTypeAttributeForDetailedResourceResponse
{
    public string Name { get; set; }
    public int AttributeId { get; set; }
    public int ResourceTypeId { get; set; }
}