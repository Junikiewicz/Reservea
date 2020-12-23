using System;

namespace Reservea.Microservices.Resources.Dtos.Responses
{
    public class ResourceAvailabilityResponse 
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsReccuring { get; set; }
        public TimeSpan? Interval { get; set; }
        public int ResourceId { get; set; }
    }
}
