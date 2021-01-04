using System.Collections.Generic;

namespace Reservea.Microservices.Resources.Dtos.Responses
{
    public class ResourceWithAvaiabilityResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<ResourceAvailabilityResponse> ResourceAvailabilities { get; set; }
        public IEnumerable<ResourceAttributeResponse> ResourceAttributes { get; set; }
    }
    
    public class ResourceAttributeResponse
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
