using AutoMapper;
using Reservea.Microservices.Resources.Dtos.Requests;
using Reservea.Microservices.Resources.Dtos.Responses;
using Reservea.Persistance.Interfaces.Repositories;
using Reservea.Persistance.Models;
using System.Collections.Generic;

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
            CreateMap<Resource, ResourceForListResponse>()
                .ForMember(dest => dest.ResourceTypeName, opts => opts.MapFrom(src => src.ResourceType.Name));
            CreateMap<Resource, ResourceForDetailedResponse>();
            CreateMap<Resource, AddResourceResponse>();
            CreateMap<Resource, ResourceWithAvaiabilityResponse>();

            CreateMap<ResourceAttribute, ResourceAttributeForDetailedResourceResponse>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Attribute.Name));

            CreateMap<ResourceTypeAttribute, ResourceTypeAttributeForDetailedResourceResponse>()
               .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Attribute.Name));

            CreateMap<Attribute, AttributeForListResponse>();
            CreateMap<Attribute, AddAttributeResponse>();

            CreateMap<ResourceType, ResourceTypeForDetailedResponse>();
            CreateMap<ResourceType, ResourceTypeForListResponse>();
            CreateMap<ResourceType, AddResourceTypeResponse>();
            CreateMap<ResourceType, ResourceTypeWithDetailsForListResponse>();
            CreateMap<ResourceType, IEnumerable<ResourceTypeAttribute>>()
                .ConstructUsing(x => x.ResourceTypeAttributes);

            CreateMap<ResourceAvailability, ResourceAvailabilityResponse>();
        }

        private void CreateMapsFromDtosToEntities()
        {
            CreateMap<AddResourceRequest, Resource>();
            CreateMap<UpdateResourceRequest, Resource>()
                .ForMember(dest => dest.ResourceAttributes, opts => opts.Ignore());

            CreateMap<ResourceAttributeForAddOrUpdateRequest, ResourceAttribute>();
            CreateMap<ResourceTypeAttributeRequest, ResourceTypeAttribute>();
            CreateMap<ResourceTypeAttributePrimaryKey, ResourceTypeAttribute>();

            CreateMap<AddAttributeRequest, Attribute>();
            CreateMap<UpdateAttributeRequest, Attribute>();

            CreateMap<UpdateResourceTypeRequest, ResourceType>()
                .ForMember(dest => dest.ResourceTypeAttributes, opts => opts.Ignore());
            CreateMap<AddResourceTypeRequest, ResourceType>();
        }
    }
}
