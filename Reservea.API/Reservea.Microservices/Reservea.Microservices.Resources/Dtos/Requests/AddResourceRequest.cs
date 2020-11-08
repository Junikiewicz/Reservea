using System.Collections.Generic;

namespace Reservea.Microservices.Resources.Dtos.Requests
{
    public class AddResourceRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerHour { get; set; }
        public int ResourceTypeId { get; set; }

        public IEnumerable<AddResourceAttributeRequest> ResourceAttributes { get; set; }
    }

    public class AddResourceAttributeRequest
    {
        public int AttributeId { get; set; }
        public string Value { get; set; }
    }
}
