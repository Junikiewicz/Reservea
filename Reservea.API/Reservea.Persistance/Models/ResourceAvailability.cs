using System;

namespace Reservea.Persistance.Models
{
    public class ResourceAvailability
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsReccuring { get; set; }
        public TimeSpan? Interval { get; set; }
        public int ResourceId { get; set; }

        public virtual Resource Resource { get; set; }
    }
}
