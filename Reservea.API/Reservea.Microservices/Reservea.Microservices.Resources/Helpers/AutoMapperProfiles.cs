using AutoMapper;
using Reservea.Microservices.Resources.Dtos.Requests;
using Reservea.Microservices.Resources.Dtos.Responses;
using Reservea.Microservices.Resources.Models;
using Reservea.Persistance.Interfaces.Repositories;
using Reservea.Persistance.Models;
using System.Collections.Generic;
using System.Linq;

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
            CreateMap<Resource, AddResourceResponse>();

            CreateMap<ResourceAttribute, ResourceAttributeForDetailedResourceResponse>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Attribute.Name));

            CreateMap<Attribute, AttributeForListResponse>();
            CreateMap<Attribute, AddAttributeResponse>();

            CreateMap<ResourceType, ResourceTypeForDetailedResponse>();
            CreateMap<ResourceType, ResourceTypeForListResponse>();
            CreateMap<ResourceType, AddResourceTypeResponse>();
            CreateMap<ResourceType, IEnumerable<ResourceTypeAttribute>>().ConstructUsing(x => x.ResourceTypeAttributes);
        }

        private void CreateMapsFromDtosToEntities()
        {
            CreateMap<AddResourceRequest, Resource>();
            CreateMap<UpdateResourceRequest, Resource>()
                .ForMember(dest => dest.ResourceAttributes, opts => opts.Ignore());

            CreateMap<ResourceAttributeForAddOrUpdateRequest, ResourceAttribute>();
            CreateMap<ResourceTypeAttributePrimaryKey, ResourceTypeAttribute>();

            CreateMap<AddAttributeRequest, Attribute>();
            CreateMap<UpdateAttributeRequest, Attribute>();

            CreateMap<UpdateResourceTypeRequest, ResourceType>();
            CreateMap<AddResourceTypeRequest, ResourceType>();
        }
    }
}
