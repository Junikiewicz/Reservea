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
    public class ResourcesService : IResourcesService
    {
        private readonly IResourcesUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ResourcesService(IResourcesUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ResourceForListResponse>> GetAllResourcesForListAsync(CancellationToken cancellationToken)
        {
            var resourcesForList = await _unitOfWork.ResourcesRepository.GetAllAsync<ResourceForListResponse>(cancellationToken);

            return resourcesForList;
        }

        public async Task<ResourceForDetailedResponse> GetResourceDetailsByIdAsync(int resourceId, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.ResourcesRepository.GetByIdAsync<int, ResourceForDetailedResponse>(resourceId, cancellationToken);

            return result;
        }

        public async Task UpdateResourceAsync(int resourceId, UpdateResourceRequest request, CancellationToken cancellationToken)
        {
            var resourceFromDatabase = await _unitOfWork.ResourcesRepository.GetByIdAsync(resourceId, cancellationToken);
            _mapper.Map(request, resourceFromDatabase);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<AddResourceResponse> AddResourceAsync(AddResourceRequest request, CancellationToken cancellationToken)
        {
            var newResource = _mapper.Map<Resource>(request);
            newResource.ResourceStatusId = (int)Common.Enums.ResourceStatus.New;

            _unitOfWork.ResourcesRepository.Add(newResource);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<AddResourceResponse>(newResource);
        }

        public async Task UpdateResourceAttributesAsync(int resourceId, UpdateResourceAttributesRequest request, CancellationToken cancellationToken)
        {
            if (request.AttributesToDelete != null)
            {
                var idsOfAttributesToDelete = request.AttributesToDelete.Select(x => new ResourceAttributePrimaryKey { ResourceId = resourceId, AttributeId = x.AttributeId });
                await _unitOfWork.ResourceAttributesRepository.DeleteByListOfIdsAsync(idsOfAttributesToDelete, cancellationToken);
            }
            if (request.AttributesToAddOrUpdate != null)
            {
                var idsOfAttributesToAddOrUpdate = request.AttributesToAddOrUpdate.Select(x => new ResourceAttributePrimaryKey { ResourceId = resourceId, AttributeId = x.AttributeId });

                var alreadyExistingResourceAttributes = await _unitOfWork.ResourceAttributesRepository.GetByListOfIdsAsync(idsOfAttributesToAddOrUpdate, cancellationToken);
                alreadyExistingResourceAttributes.ForEach(x => x.Value = request.AttributesToAddOrUpdate.Single(y => y.AttributeId == x.AttributeId).Value);
                _unitOfWork.ResourceAttributesRepository.UpdateRange(alreadyExistingResourceAttributes);

                var attributesToAdd = request.AttributesToAddOrUpdate.Where(x => !alreadyExistingResourceAttributes.Any(y => x.AttributeId == y.AttributeId));
                var attributesToAddEntities = _mapper.Map<IEnumerable<ResourceAttribute>>(attributesToAdd);
                attributesToAddEntities.ForEach(x => x.ResourceId = resourceId);
                _unitOfWork.ResourceAttributesRepository.AddRange(attributesToAddEntities);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
