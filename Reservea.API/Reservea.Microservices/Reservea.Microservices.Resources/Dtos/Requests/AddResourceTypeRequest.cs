using System.Collections.Generic;

namespace Reservea.Microservices.Resources.Dtos.Requests
{
    public class AddResourceTypeRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ResourceTypeAttributeRequest> ResourceTypeAttributes { get; set; }
    }
}
