﻿namespace Reservea.Microservices.Resources.Dtos.Responses
{
    public class ResourceForListResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ResourceStatusId { get; set; }
        public int ResourceTypeId { get; set; }
        public string ResourceTypeName { get; set; }
    }
}
