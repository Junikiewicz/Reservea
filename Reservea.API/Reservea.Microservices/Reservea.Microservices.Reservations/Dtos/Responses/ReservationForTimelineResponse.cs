using System;

namespace Reservea.Microservices.Reservations.Dtos.Responses
{
    public class ReservationForTimelineResponse
    {
        public int ResourceId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
