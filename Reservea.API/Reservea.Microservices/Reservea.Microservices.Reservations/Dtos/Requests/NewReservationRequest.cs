using System;

namespace Reservea.Microservices.Reservations.Dtos.Requests
{
    public class NewReservationRequest
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int ResourceId { get; set; }
    }
}
