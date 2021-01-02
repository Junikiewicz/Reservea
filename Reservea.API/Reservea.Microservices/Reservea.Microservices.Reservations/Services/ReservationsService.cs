using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Reservea.Common.Extensions;
using Reservea.Common.Helpers;
using Reservea.Common.Interfaces;
using Reservea.Common.Mails;
using Reservea.Common.Mails.Models;
using Reservea.Microservices.Reservations.Dtos.Requests;
using Reservea.Microservices.Reservations.Dtos.Responses;
using Reservea.Microservices.Reservations.Interfaces.Services;
using Reservea.Persistance.Interfaces.UnitsOfWork;
using Reservea.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IMailSendingService _mailSendingService;
        private readonly IConfiguration _configuration;
        private readonly CannonService _cannonService;

        public ReservationsService(IConfiguration configuration, IReservationsUnitOfWork reservationsUnitOfWork, IMapper mapper, IMailSendingService mailSendingService, CannonService cannonService)
        {
            _reservationsUnitOfWork = reservationsUnitOfWork;
            _configuration = configuration;
            _mapper = mapper;
            _mailSendingService = mailSendingService;
            _cannonService = cannonService;
        }

        public async Task<IEnumerable<ReservationForListResponse>> GetReservationsForListAsync(CancellationToken cancellationToken)
        {
            return await _reservationsUnitOfWork.ReservationsRepository.GetAllAsync<ReservationForListResponse>(cancellationToken);
        }

        public async Task<IEnumerable<ReservationForListResponse>> GetUserReservations(int userId, CancellationToken cancellationToken)
        {
            return await _reservationsUnitOfWork.ReservationsRepository.GetAsync<ReservationForListResponse>(x => x.UserId == userId, cancellationToken);
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

            //send confirmation mail
            _cannonService.FireAsync<IReservationsUnitOfWork>(async (reservationsUnitOfWork) =>
            {
                var reservationDetails = await _reservationsUnitOfWork.ReservationsRepository
                     .GetAsync(x => newReservations.Select(y => y.Id).Contains(x.Id),
                     cancellationToken,
                     x => x.Include(y => y.User).Include(y => y.Resource));

                var model = new ReservationConfirmationMailTemplateModel
                {
                    Subject = "Potwierdzenie rezerwacji",
                    Name = reservationDetails.First().User.FirstName,
                    ReservationsListUrl = $"{_configuration["FrontendUrl"]}/user-reservations",
                    To = $"{reservationDetails.First().User.FirstName} {reservationDetails.First().User.LastName}",
                    ToAddress = reservationDetails.First().User.Email,
                    Reservations = reservationDetails.Select(x => new ReservationModel { Id = x.Id, Start = x.Start, End = x.End, ResourceName = x.Resource.Name })
                };

                await _mailSendingService.SendMailFromTemplateAsync(MailTemplates.ReservationConfirmation, model);
            });
        }
    }
}
