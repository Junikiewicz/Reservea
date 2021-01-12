using AutoMapper;
using Reservea.Microservices.CMS.Dtos.Requests;
using Reservea.Microservices.CMS.Dtos.Responses;
using Reservea.Microservices.CMS.Models;
using Reservea.Persistance.Models;

namespace Reservea.Microservices.Reservations.Helpers
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
            CreateMap<TextFieldContent, TextFieldContentResponse>();
            CreateMap<Photo, PhotoResponse>();
            CreateMap<UserRate, UserRateForHomePageResponse>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.User.FirstName + " " + src.User.LastName));
            CreateMap<UserRate, UserRateForListResponse>();
            CreateMap<UserRate, UserRateForRandomPick>();
        }

        private void CreateMapsFromDtosToEntities()
        {
            CreateMap<AddPhotoRequest, Photo>();
            CreateMap<UpdateTextFieldContentRequest, TextFieldContent>();
            CreateMap<CreateUserRateRequest, UserRate>();
            CreateMap<UserRateUpdateRequest, UserRate>();
        }
    }
}
