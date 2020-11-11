﻿using AutoMapper;
using Reservea.Microservices.Resources.Dtos.Requests;
using Reservea.Microservices.Resources.Dtos.Responses;
using Reservea.Microservices.Resources.Interfaces.Services;
using Reservea.Persistance.Interfaces.UnitsOfWork;
using Reservea.Persistance.Models;
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

        public async Task<IEnumerable<AttributeForListResponse>> GetAllAttributesForListAsync(CancellationToken cancellationToken)
        {
            return await _unitOfWork.AttributesRepository.GetAllAsync<AttributeForListResponse>(cancellationToken);
        }

        public async Task<AddAttributeResponse> AddAttributeAsync(AddAttributeRequest request, CancellationToken cancellationToken)
        {
            var newAttribute = _mapper.Map<Attribute>(request);

            _unitOfWork.AttributesRepository.Add(newAttribute);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<AddAttributeResponse>(newAttribute);
        }

        public async Task UpdateAttributeAsync(int id, UpdateAttributeRequest request, CancellationToken cancellationToken)
        {
            var attributeFromDatabase = await _unitOfWork.AttributesRepository.GetByIdAsync(id, cancellationToken);

            _mapper.Map(request, attributeFromDatabase);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveAttributeAsync(int id, CancellationToken cancellationToken)
        {
            await _unitOfWork.AttributesRepository.RemoveByIdAsync(id, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}