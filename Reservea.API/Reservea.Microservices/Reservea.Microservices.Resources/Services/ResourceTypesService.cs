using AutoMapper;
using Reservea.Common.Extensions;
using Reservea.Microservices.Resources.Dtos.Requests;
using Reservea.Microservices.Resources.Dtos.Responses;
using Reservea.Microservices.Resources.Interfaces.Services;
using Reservea.Persistance.Interfaces.Repositories;
using Reservea.Persistance.Interfaces.UnitsOfWork;
using Reservea.Persistance.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.Resources.Services
{
    public class ResourceTypesService : IResourceTypesService
    {
        private readonly IResourcesUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ResourceTypesService(IResourcesUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ResourceTypeForListResponse>> GetAllResourceTypesForListAsync(CancellationToken cancellationToken)
        {
            var resourceTypeForList = await _unitOfWork.ResourceTypesRepository.GetAllAsync<ResourceTypeForListResponse>(cancellationToken);

            return resourceTypeForList;
        }

        public async Task<ResourceTypeForDetailedResponse> GetResourceTypeDetailsAsync(int resourceTypeId, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.ResourceTypesRepository.GetByIdAsync<int, ResourceTypeForDetailedResponse>(resourceTypeId, cancellationToken);

            return result;
        }

        public async Task UpdateResourceTypeAsync(int resourceTypeId, UpdateResourceTypeRequest request, CancellationToken cancellationToken)
        {
            var resourceTypeFromDatabase = await _unitOfWork.ResourceTypesRepository.GetByIdAsync(resourceTypeId, cancellationToken);
            _mapper.Map(request, resourceTypeFromDatabase);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<AddResourceTypeResponse> AddResourceTypeAsync(AddResourceTypeRequest request, CancellationToken cancellationToken)
        {
            var newResourceType = _mapper.Map<ResourceType>(request);

            _unitOfWork.ResourceTypesRepository.Add(newResourceType);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<AddResourceTypeResponse>(newResourceType);
        }

        public async Task DeleteResourceTypeAsync(int resourceTypeId, CancellationToken cancellationToken)
        {
            await _unitOfWork.ResourceTypesRepository.RemoveByIdAsync(resourceTypeId, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateResourceTypeAttributesAsync(int resourceTypeId, UpdateResourceTypeAttributesRequest request, CancellationToken cancellationToken)
        {
            if (request.AttributesToDelete != null)
            {
                var idsOfAttributesToDelete = request.AttributesToDelete.Select(x => new ResourceTypeAttributePrimaryKey { ResourceTypeId = resourceTypeId, AttributeId = x });
                await _unitOfWork.ResourceTypeAttributesRepository.RemoveByListOfIdsAsync(idsOfAttributesToDelete, cancellationToken);
            }
            if (request.AttributesToAdd != null)
            {
                var idsOfAttributesToAdd = request.AttributesToAdd.Select(x => new ResourceTypeAttributePrimaryKey { ResourceTypeId = resourceTypeId, AttributeId = x });

                var attributesToAddEntities = _mapper.Map<IEnumerable<ResourceTypeAttribute>>(idsOfAttributesToAdd);
                attributesToAddEntities.ForEach(x => x.ResourceTypeId = resourceTypeId);
                _unitOfWork.ResourceTypeAttributesRepository.AddRange(attributesToAddEntities);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
