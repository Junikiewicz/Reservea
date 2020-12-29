using AutoMapper;
using Reservea.Microservices.Users.Dtos.Requests;
using Reservea.Microservices.Users.Dtos.Responses;
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
            CreateMap<Role, RoleResponse>();
        }

        private void CreateMapsFromDtosToEntities()
        {
            CreateMap<UpdateUserRequest, User>();
        }
    }
}
