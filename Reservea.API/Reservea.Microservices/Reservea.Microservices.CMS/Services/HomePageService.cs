using AutoMapper;
using Reservea.Microservices.CMS.Dtos.Requests;
using Reservea.Microservices.CMS.Dtos.Responses;
using Reservea.Microservices.CMS.Interfaces.Services;
using Reservea.Persistance.Interfaces.UnitsOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.CMS.Services
{
    public class HomePageService : IHomePageService
    {
        private readonly ICmsUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HomePageService(ICmsUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TextFieldContentResponse>> GetAllTextFieldsContents(CancellationToken cancellationToken)
        {
            return await _unitOfWork.TextFieldsContentsRepository.GetAllAsync<TextFieldContentResponse>(cancellationToken);
        }

        public async Task<TextFieldContentResponse> GetTextFieldContent(string name, CancellationToken cancellationToken)
        {
            return await _unitOfWork.TextFieldsContentsRepository.GetSingleAsync<TextFieldContentResponse>(x => x.Name == name, cancellationToken);
        }

        public async Task UpdateTextFieldContent(IEnumerable<UpdateTextFieldContentRequest> request, CancellationToken cancellationToken)
        {
            var entities = await _unitOfWork.TextFieldsContentsRepository.GetAsync(x => request.Select(y => y.Id).Contains(x.Id), cancellationToken);

            foreach (var entity in entities)
            {
                _mapper.Map(request.Single(x => x.Id == entity.Id), entity);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
