using System.Collections.Generic;

namespace Reservea.Microservices.Resources.Dtos.Responses
{
    public class ResourceWithAvaiabilityResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ResourceAvailabilityResponse> ResourceAvailabilities { get; set; }
    }
}
