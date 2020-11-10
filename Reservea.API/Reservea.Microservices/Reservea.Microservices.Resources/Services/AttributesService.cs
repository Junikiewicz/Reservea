using AutoMapper;
using Reservea.Microservices.Resources.Dtos.Requests;
using Reservea.Microservices.Resources.Dtos.Responses;
using Reservea.Microservices.Resources.Interfaces.Services;
using Reservea.Persistance.Interfaces.UnitsOfWork;
using Reservea.Persistance.Models;
using Reservea.Persistance.UnitsOfWork;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.Resources.Services
{
    public class AttributesService : IAttributesService
    {
        private readonly IResourcesUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AttributesService(IResourcesUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AttributeForListResponse>> GetAllAttributesForList(CancellationToken cancellationToken)
        {
            return await _unitOfWork.AttributesRepository.GetAllAsync<AttributeForListResponse>(cancellationToken);
        }

        public async Task<AddAttributeResponse> AddAttribute(AddAttributeRequest request, CancellationToken cancellationToken)
        {
            var newAttribute = _mapper.Map<Attribute>(request);

            _unitOfWork.AttributesRepository.Add(newAttribute);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<AddAttributeResponse>(newAttribute);
        }

        public async Task EditAttribute(int id, EditAttributeRequest request, CancellationToken cancellationToken)
        {
            var attributeFromDatabase = await _unitOfWork.AttributesRepository.GetByIdAsync(id, cancellationToken);

            _ = _mapper.Map(request, attributeFromDatabase);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAttribute(int id, CancellationToken cancellationToken)
        {
            await _unitOfWork.AttributesRepository.DeleteByIdAsync(id, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
