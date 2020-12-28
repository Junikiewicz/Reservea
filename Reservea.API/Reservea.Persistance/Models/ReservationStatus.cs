using System.Collections.Generic;

namespace Reservea.Persistance.Models
{
    public class ReservationStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
