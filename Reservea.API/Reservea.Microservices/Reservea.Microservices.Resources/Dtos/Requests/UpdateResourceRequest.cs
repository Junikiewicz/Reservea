using System;
using System.Collections.Generic;

namespace Reservea.Microservices.Resources.Dtos.Requests
{
    public class UpdateResourceRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal PricePerHour { get; set; }
        public int ResourceTypeId { get; set; }
        public int ResourceStatusId { get; set; }
        public IEnumerable<ResourceAttributeForAddOrUpdateRequest> ResourceAttributes { get; set; }
        public IEnumerable<ResourceAvaiabilityForAddOrUpdateRequest> ResourceAvaiabilities { get; set; }
    }

    public class ResourceAvaiabilityForAddOrUpdateRequest
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsReccuring { get; set; }
        public TimeSpan? Interval { get; set; }
    }

    public class ResourceAttributeForAddOrUpdateRequest
    {
        public int AttributeId { get; set; }
        public string Value { get; set; }
    }
}