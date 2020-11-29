using System.Collections.Generic;

namespace Reservea.Microservices.Resources.Dtos.Requests
{
    public class UpdateResourceRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerHour { get; set; }
        public int ResourceTypeId { get; set; }
        public int ResourceStatusId { get; set; }
        public IEnumerable<ResourceAttributeForAddOrUpdateRequest> ResourceAttributes { get; set; }
    }

    public class ResourceAttributeForAddOrUpdateRequest
    {
        public int AttributeId { get; set; }
        public string Value { get; set; }
    }
}