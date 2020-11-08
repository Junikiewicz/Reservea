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
            CreateMap<Resource, ResourceForListResponse>();
            CreateMap<Resource, ResourceForDetailedResponse>();
            CreateMap<ResourceAttribute, ResourceAttributeForDetailedResourceResponse>();

            CreateMap<AddResourceRequest, Resource>();
            CreateMap<UpdateResourceRequest, Resource>();
            CreateMap<ResourceAttributeForAddOrUpdateRequest, ResourceAttribute >();
        }
    }
}
