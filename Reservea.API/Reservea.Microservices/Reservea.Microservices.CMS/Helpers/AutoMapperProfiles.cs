using AutoMapper;
using Reservea.Microservices.CMS.Dtos.Requests;
using Reservea.Microservices.CMS.Dtos.Responses;
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
        }

        private void CreateMapsFromDtosToEntities()
        {
            CreateMap<AddPhotoRequest, Photo>();
            CreateMap<UpdateTextFieldContentRequest, TextFieldContent>();
        }
    }
}
