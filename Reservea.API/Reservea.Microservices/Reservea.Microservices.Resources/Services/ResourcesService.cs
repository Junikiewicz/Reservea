using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Reservea.Common.Extensions;
using Reservea.Microservices.Resources.Dtos.Requests;
using Reservea.Microservices.Resources.Dtos.Responses;
using Reservea.Microservices.Resources.Interfaces.Services;
using Reservea.Persistance.Interfaces.UnitsOfWork;
using Reservea.Persistance.Models;
using System;
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
            var resourcesForList = await _unitOfWork.ResourcesRepository.GetAsync<ResourceForListResponse>(x => x.ResourceStatusId != (int)Enums.ResourceStatus.Removed, cancellationToken);

            return resourcesForList;
        }

        public async Task<ResourceForDetailedResponse> GetResourceDetailsAsync(int resourceId, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.ResourcesRepository.GetSingleAsync<ResourceForDetailedResponse>(x => x.Id == resourceId, cancellationToken);

            result.ResourceAttributes = result.ResourceAttributes.Where(x => x.IsActive).ToList();//TEMP

            return result;
        }

        public async Task<IEnumerable<ResourceWithAvaiabilityResponse>> GetResourcesAvailabilityAsync(int resourceTypeId, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.ResourcesRepository.GetAsync(x => x.ResourceTypeId == resourceTypeId, cancellationToken, x => x.Include(x => x.ResourceAvailabilities)); //TEMP

            return _mapper.Map<IEnumerable<ResourceWithAvaiabilityResponse>>(result);
        }


        public async Task<ResourceAvailabilityResponse> GetResourceAvailabilityAsync(int resourceId, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.ResourcesRepository.GetSingleAsync(x => x.Id == resourceId, cancellationToken, x => x.Include(x => x.ResourceAvailabilities)); //TEMP

            return _mapper.Map<ResourceAvailabilityResponse>(result.ResourceAttributes);
        }


        public async Task<IEnumerable<ResourceAttributeForDetailedResourceResponse>> GetResourceAttributesForTypeChange(int resourceId, int resourceTypeId, CancellationToken cancellationToken)
        {
            var resourceAttributes = await _unitOfWork.ResourceAttributesRepository.GetAsync(x => x.ResourceId == resourceId, cancellationToken);
            var resourceTypeAttributes = await _unitOfWork.ResourceTypesRepository.GetResourceTypeAttributes(resourceTypeId, cancellationToken);
            var attributesList = new List<ResourceAttributeForDetailedResourceResponse>();

            foreach (var attribute in resourceTypeAttributes)
            {
                var alreadyPresentAttribute = resourceAttributes.SingleOrDefault(x => x.ResourceId == resourceId && x.AttributeId == attribute.Id);

                var newAttribute = new ResourceAttributeForDetailedResourceResponse
                {
                    AttributeId = attribute.Id,
                    ResourceId = resourceId,
                    Name = attribute.Name,
                    Value = alreadyPresentAttribute != null ? alreadyPresentAttribute.Value : ""
                };

                attributesList.Add(newAttribute);
            }

            return attributesList;
        }

        public async Task UpdateResourceAsync(int resourceId, UpdateResourceRequest request, CancellationToken cancellationToken)
        {
            if (request.ResourceStatusId == (int)Enums.ResourceStatus.Removed)
            {
                throw new Exception("Żeby usunać zasób skorzystaj z endpointu delete.");//temp (until validation implementation)
            }

            var resourceFromDatabase = await _unitOfWork.ResourcesRepository.GetSingleAsync(
                predicate: x => x.Id == resourceId,
                cancellationToken,
                include: i => i.Include(x => x.ResourceAttributes).Include(x => x.ResourceAvailabilities));

            // attributes
            var resourceTypeAttributesIds = await _unitOfWork.ResourceTypesRepository.GetResourceTypeAttributesIds(request.ResourceTypeId, cancellationToken);
            var attributesToSetInactive = resourceFromDatabase.ResourceAttributes.Where(x => !resourceTypeAttributesIds.Contains(x.AttributeId));
            foreach (var newAttributeId in resourceTypeAttributesIds)
            {
                var attributeValueToSet = request.ResourceAttributes?.SingleOrDefault(x => x.AttributeId == newAttributeId)?.Value;
                var alreadyPresentAttribute = resourceFromDatabase.ResourceAttributes.SingleOrDefault(x => x.ResourceId == resourceId && x.AttributeId == newAttributeId);
                if (alreadyPresentAttribute != null)
                {
                    alreadyPresentAttribute.Value = attributeValueToSet;
                    alreadyPresentAttribute.IsActive = true;
                }
                else
                {
                    resourceFromDatabase.ResourceAttributes.Add(new ResourceAttribute { AttributeId = newAttributeId, ResourceId = resourceId, Value = attributeValueToSet, IsActive = true });
                }
            }

            attributesToSetInactive.ForEach(x => x.IsActive = false);

            // avalabilities
            if (request.ResourceAvailabilities is null)
            {
                _unitOfWork.ResourceAvailabilitiesRepository.RemoveRange(resourceFromDatabase.ResourceAvailabilities);
            }
            else
            {
                var avalabilitesToDelete = resourceFromDatabase.ResourceAvailabilities.Where(x => !request.ResourceAvailabilities.Select(x => x.Id).Contains(x.Id));
                if (avalabilitesToDelete.Any())
                {
                    _unitOfWork.ResourceAvailabilitiesRepository.RemoveRange(avalabilitesToDelete);
                }

                var avalabilitesToUpdate = resourceFromDatabase.ResourceAvailabilities.Where(x => request.ResourceAvailabilities.Select(x => x.Id).Contains(x.Id));
                foreach (var avalability in avalabilitesToUpdate)
                {
                    var newAvability = request.ResourceAvailabilities.Single(x => x.Id == avalability.Id);
                    _mapper.Map(newAvability, avalability);
                }

                var avalabilitesToAdd = request.ResourceAvailabilities.Where(x => x.Id == -1);
                foreach (var avability in avalabilitesToAdd)
                {
                    avability.Id = 0;
                    resourceFromDatabase.ResourceAvailabilities.Add(_mapper.Map<ResourceAvailability>(avability));
                }
            }

            _mapper.Map(request, resourceFromDatabase);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<AddResourceResponse> AddResourceAsync(AddResourceRequest request, CancellationToken cancellationToken)
        {
            var newResource = _mapper.Map<Resource>(request);
            newResource.ResourceStatusId = (int)Enums.ResourceStatus.New;
            newResource.ResourceAttributes.ForEach(x => x.IsActive = true);
            newResource.ResourceAvailabilities.ForEach(x => x.Id = 0);

            _unitOfWork.ResourcesRepository.Add(newResource);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<AddResourceResponse>(newResource);
        }

        public async Task RemoveResourceAsync(int resourceId, CancellationToken cancellationToken)
        {
            var resourceFromDatabase = await _unitOfWork.ResourcesRepository.GetSingleAsync(x => x.Id == resourceId, cancellationToken, include: i => i.Include(x => x.ResourceAttributes));

            resourceFromDatabase.ResourceStatusId = (int)Enums.ResourceStatus.Removed;
            _unitOfWork.ResourceAttributesRepository.RemoveRange(resourceFromDatabase.ResourceAttributes);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
