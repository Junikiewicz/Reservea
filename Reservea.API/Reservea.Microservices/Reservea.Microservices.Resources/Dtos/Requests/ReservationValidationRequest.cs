using System;

namespace Reservea.Microservices.Resources.Dtos.Requests
{
    public class ReservationValidationRequest
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int ResourceId { get; set; }
    }
}
