using System;

namespace Reservea.Microservices.Reservations.Dtos.Responses
{
    public class ReservationForListResponse
    {
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string ResourceName { get; set; }
        public int ReservationStatusId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
