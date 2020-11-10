using AutoMapper;
using Reservea.Microservices.Resources.Dtos.Requests;
using Reservea.Microservices.Resources.Dtos.Responses;
using Reservea.Persistance.Models;

namespace Reservea.Microservices.Resources.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMapsFromEntitiesToDtos();
            CreateMapsFromDtosToEntities();
        }

        private void CreateMapsFromEntitiesToDtos()
        {
            CreateMap<Resource, ResourceForListResponse>();
            CreateMap<Resource, ResourceForDetailedResponse>();

            CreateMap<ResourceAttribute, ResourceAttributeForDetailedResourceResponse>();

            CreateMap<Attribute, AttributeForListResponse>();
            CreateMap<Attribute, AddAttributeResponse>();

            CreateMap<ResourceType, ResourceTypeForDetailedResponse>();
            CreateMap<ResourceType, ResourceTypeForListResponse>();
            CreateMap<ResourceType, AddResourceTypeResponse>();
        }

        private void CreateMapsFromDtosToEntities()
        {
            CreateMap<AddResourceRequest, Resource>();
            CreateMap<UpdateResourceRequest, Resource>();

            CreateMap<ResourceAttributeForAddOrUpdateRequest, ResourceAttribute>();

            CreateMap<AddAttributeRequest, Attribute>();
            CreateMap<EditAttributeRequest, Attribute>();

            CreateMap<UpdateResourceTypeRequest, ResourceType>();
            CreateMap<AddResourceTypeRequest, ResourceType>();
        }
    }
}
