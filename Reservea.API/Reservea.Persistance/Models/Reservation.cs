using System;

namespace Reservea.Persistance.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int ResourceId { get; set; }
        public int UserId { get; set; }
        public int ReservationStatusId { get; set; }

        public virtual ReservationStatus ReservationStatus { get; set; }
        public virtual Resource Resource { get; set; }
        public virtual User User { get; set; }

    }
}
