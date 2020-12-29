using System.Collections.Generic;

namespace Reservea.Microservices.Users.Dtos.Responses
{
    public class UserForListResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<int> Roles { get; set; }
    }
}
