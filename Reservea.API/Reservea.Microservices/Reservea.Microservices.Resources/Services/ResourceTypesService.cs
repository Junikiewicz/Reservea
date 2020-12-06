using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Reservea.Common.Extensions;
using Reservea.Microservices.Resources.Dtos.Requests;
using Reservea.Microservices.Resources.Dtos.Responses;
using Reservea.Microservices.Resources.Interfaces.Services;
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
            var result = await _unitOfWork.ResourceTypesRepository.GetSingleAsync<ResourceTypeForDetailedResponse>(x => x.Id == resourceTypeId, cancellationToken);

            return result;
        }

        public async Task<IEnumerable<AttributeForListResponse>> GetResourceTypeAttributesAsync(int resourceTypeId, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.ResourceTypesRepository.GetResourceTypeAttributes(resourceTypeId, cancellationToken);

            return _mapper.Map<IEnumerable<AttributeForListResponse>>(result);
        }

        public async Task UpdateResourceTypeAsync(int resourceTypeId, UpdateResourceTypeRequest request, CancellationToken cancellationToken)
        {
            var resourceTypeFromDatabase = await _unitOfWork.ResourceTypesRepository.GetSingleAsync(x => x.Id == resourceTypeId, cancellationToken, i => i.Include(x => x.ResourceTypeAttributes));
            _mapper.Map(request, resourceTypeFromDatabase);

            var attributesToDelete = resourceTypeFromDatabase.ResourceTypeAttributes
                .Where(x => !request.ResourceTypeAttributes
                    .Select(s => s.AttributeId)
                    .Contains(x.AttributeId));

            var attributesToAdd = request.ResourceTypeAttributes
                .Where(x => !resourceTypeFromDatabase.ResourceTypeAttributes
                    .Select(s => s.AttributeId)
                    .Contains(x.AttributeId))
                .Select(x => new ResourceTypeAttribute { AttributeId = x.AttributeId, ResourceTypeId = resourceTypeId });

            if (attributesToAdd.Any() || attributesToDelete.Any())
            {
                var resources = (await _unitOfWork.ResourcesRepository.GetAsync(x => x.ResourceTypeId == resourceTypeId, cancellationToken, i => i.Include(x => x.ResourceAttributes)));
                var allAttributes = new List<ResourceAttribute>();
                resources.ForEach(x => allAttributes = allAttributes.Union(x.ResourceAttributes).ToList());
                allAttributes.Where(x => attributesToAdd.Select(s => s.AttributeId).Contains(x.AttributeId)).ForEach(x => x.IsActive = true);
                allAttributes.Where(x => attributesToDelete.Select(s => s.AttributeId).Contains(x.AttributeId)).ForEach(x => x.IsActive = false);

                foreach (var resource in resources)
                {
                    var resourceAttributesToAdd = attributesToAdd
                        .Where(x => !resource.ResourceAttributes.Select(x => x.AttributeId).Contains(x.AttributeId))
                        .Select(x => new ResourceAttribute { AttributeId = x.AttributeId, IsActive = true, ResourceId = resource.Id });
                    resourceAttributesToAdd.ForEach(x => resource.ResourceAttributes.Add(x));
                }
            }
            attributesToAdd.ForEach(x => resourceTypeFromDatabase.ResourceTypeAttributes.Add(x));
            _unitOfWork.ResourceTypeAttributesRepository.RemoveRange(attributesToDelete);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<AddResourceTypeResponse> AddResourceTypeAsync(AddResourceTypeRequest request, CancellationToken cancellationToken)
        {
            var newResourceType = _mapper.Map<ResourceType>(request);

            _unitOfWork.ResourceTypesRepository.Add(newResourceType);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<AddResourceTypeResponse>(newResourceType);
        }

        public async Task RemoveResourceTypeAsync(int resourceTypeId, CancellationToken cancellationToken)
        {
            //await _unitOfWork.ResourceTypesRepository.RemoveByIdAsync(resourceTypeId, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
