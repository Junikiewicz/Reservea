using System.Collections.Generic;

namespace Reservea.Microservices.Resources.Dtos.Requests
{
    public class UpdateResourceAttributesRequest
    {
        public IEnumerable<ResourceAttributeForAddOrUpdateRequest> AttributesToAddOrUpdate { get; set; }
        public IEnumerable<ResourceAttributeDeleteRequst> AttributesToDelete { get; set; }
    }

    public class ResourceAttributeForAddOrUpdateRequest 
    {
        public int AttributeId { get; set; }
        public string Value { get; set; }
    }

    public class ResourceAttributeDeleteRequst
    {
        public int AttributeId { get; set; }
    }
}
