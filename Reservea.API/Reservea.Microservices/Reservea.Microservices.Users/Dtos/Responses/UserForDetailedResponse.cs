using System.Collections.Generic;

namespace Reservea.Microservices.Users.Dtos.Responses
{
    public class UserForDetailedResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<RoleResponse> Roles { get; set; }
    }
}
