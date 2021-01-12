using AutoMapper;
using Reservea.Microservices.CMS.Dtos.Requests;
using Reservea.Microservices.CMS.Dtos.Responses;
using Reservea.Microservices.CMS.Interfaces.Services;
using Reservea.Microservices.CMS.Models;
using Reservea.Persistance.Interfaces.UnitsOfWork;
using Reservea.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.CMS.Services
{

    public partial class UserRatesService : IUserRatesService
    {
        private readonly ICmsUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static Random _random;//temp

        public UserRatesService(ICmsUnitOfWork unitOfWork, IMapper mapper)
        {
            if (_random is null)
            {
                _random = new Random();
            }

            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddUserRateAsync(CreateUserRateRequest request, int userId, CancellationToken cancellationToken)
        {
            var userRate = _mapper.Map<UserRate>(request);
            userRate.IsVisible = false;
            userRate.UserId = userId;

            _unitOfWork.UserRatesRepository.Add(userRate);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateUserRatesAsync(IEnumerable<UserRateUpdateRequest> request, CancellationToken cancellationToken)
        {
            var userRatesToUpdate = await _unitOfWork.UserRatesRepository.GetAsync(x => request.Select(s => s.Id).Contains(x.Id), cancellationToken);

            foreach (var rate in request)
            {
                _mapper.Map(rate, userRatesToUpdate.Single(x => x.Id == rate.Id));
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<UserRateForListResponse>> GetAllUserRatesAsync(CancellationToken cancellationToken)
        {
            return await _unitOfWork.UserRatesRepository.GetAllAsync<UserRateForListResponse>(cancellationToken);
        }

        public async Task<IEnumerable<UserRateForHomePageResponse>> GetUserRatesForHomepageAsync(CancellationToken cancellationToken)
        {
            var userRates = (await _unitOfWork.UserRatesRepository.GetAsync<UserRateForRandomPick>(x => x.IsVisible && x.IsAllowedToBeShared, cancellationToken)).ToList();

            var idsToReturn = new List<int>();

            while (idsToReturn.Count < 3 && idsToReturn.Count < userRates.Count)
            {
                var randomNumber = _random.Next(userRates.Count);
                if (!idsToReturn.Contains(userRates[randomNumber].Id))
                {
                    idsToReturn.Add(userRates[randomNumber].Id);
                }
            }

            return (await _unitOfWork.UserRatesRepository.GetAsync<UserRateForHomePageResponse>(x => idsToReturn.Contains(x.Id), cancellationToken));
        }
    }
}
