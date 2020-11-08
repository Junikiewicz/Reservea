using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Reservea.Persistance.Users.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
