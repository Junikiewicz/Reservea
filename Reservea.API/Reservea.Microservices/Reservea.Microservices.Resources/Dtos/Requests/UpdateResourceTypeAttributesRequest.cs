using System.Collections.Generic;

namespace Reservea.Microservices.Resources.Dtos.Requests
{
    public class UpdateResourceTypeAttributesRequest
    {
        public IEnumerable<int> AttributesToAdd { get; set; }
        public IEnumerable<int> AttributesToDelete { get; set; }
    }
}
