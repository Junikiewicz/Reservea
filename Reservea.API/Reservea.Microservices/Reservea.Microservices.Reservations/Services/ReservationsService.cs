using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Reservea.Common.Extensions;
using Reservea.Microservices.Reservations.Dtos.Requests;
using Reservea.Microservices.Reservations.Dtos.Responses;
using Reservea.Microservices.Reservations.Interfaces.Services;
using Reservea.Persistance.Interfaces.UnitsOfWork;
using Reservea.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.Reservations.Services
{
    public class ReservationsService : IReservationsService
    {
        private readonly IReservationsUnitOfWork _reservationsUnitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public ReservationsService(IConfiguration configuration, IReservationsUnitOfWork reservationsUnitOfWork, IMapper mapper)
        {
            _reservationsUnitOfWork = reservationsUnitOfWork;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReservationForListResponse>> GetReservationsForListAsync(CancellationToken cancellationToken)
        {
            return await _reservationsUnitOfWork.ReservationsRepository.GetAllAsync<ReservationForListResponse>(cancellationToken);
        }

        public async Task<IEnumerable<ReservationForTimelineResponse>> GetResourceTypeReservationsAsync(int resourceTypeId, CancellationToken cancellationToken)
        {
            return await _reservationsUnitOfWork.ReservationsRepository.GetAsync<ReservationForTimelineResponse>(x => x.Resource.ResourceTypeId == resourceTypeId, cancellationToken);
        }

        public async Task CreateReservationAsync(IEnumerable<NewReservationRequest> reservations, int userId, CancellationToken cancellationToken)
        {
            var newReservations = _mapper.Map<IEnumerable<Reservation>>(reservations);
            newReservations.ForEach(x => { x.UserId = userId; x.ReservationStatusId = (int)Enums.ReservationStatus.New; });

            var apiGatewayUrl = _configuration.GetSection("ApiGatewayUrl").Value;

            //validate avaiability
            var isValid = false;
            using (var httpClient = new HttpClient())
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(reservations), Encoding.UTF8, "application/json");
                using (var httpResponse = await httpClient.PostAsync($"{apiGatewayUrl}/api/resources/Resources/validate-avaiability", stringContent))
                {
                    var response = await httpResponse.Content.ReadAsStringAsync();
                    isValid = bool.Parse(response);
                }
            }

            if (!isValid)
            {
                throw new Exception("temp1");
            }

            //validate collisions
            foreach (var reservation in newReservations)
            {
                if (await _reservationsUnitOfWork.ReservationsRepository.CheckIfCollidingReservationExists(reservation, cancellationToken))
                {
                    throw new Exception("Temp2");
                }
            }

            //save reservation
            _reservationsUnitOfWork.ReservationsRepository.AddRange(newReservations);

            await _reservationsUnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
