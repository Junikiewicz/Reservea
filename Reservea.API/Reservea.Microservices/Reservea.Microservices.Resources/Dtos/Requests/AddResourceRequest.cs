using System.Collections.Generic;

namespace Reservea.Microservices.Resources.Dtos.Requests
{
    public class AddResourceRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerHour { get; set; }
        public int ResourceTypeId { get; set; }

        public IEnumerable<ResourceAttributeForAddOrUpdateRequest> ResourceAttributes { get; set; }
        public IEnumerable<ResourceAvaiabilityForAddOrUpdateRequest> ResourceAvaiabilities { get; set; }
    }
}
