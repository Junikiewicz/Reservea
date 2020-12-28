using AutoMapper;
using Reservea.Microservices.Reservations.Dtos.Requests;
using Reservea.Microservices.Reservations.Dtos.Responses;
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
            CreateMap<Reservation, ReservationForTimelineResponse>();
            CreateMap<Reservation, ReservationForListResponse>()
                .ForMember(dest => dest.ResourceName, opts => opts.MapFrom(src => src.Resource.Name))
                .ForMember(dest => dest.Username, opts => opts.MapFrom(src => src.User.Email));
        }

        private void CreateMapsFromDtosToEntities()
        {
            CreateMap<NewReservationRequest, Reservation>();
        }
    }
}
