using System.Collections.Generic;

namespace Reservea.Persistance.Models
{
    public class ResourceStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }
    }
}
