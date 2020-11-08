using AutoMapper;
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

        public async Task AddResource(AddResourceRequest request, CancellationToken cancellationToken)
        {
            var newResource = _mapper.Map<Resource>(request);
            newResource.ResourceStatusId = 1;

            _unitOfWork.ResourcesRepository.Add(newResource);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
