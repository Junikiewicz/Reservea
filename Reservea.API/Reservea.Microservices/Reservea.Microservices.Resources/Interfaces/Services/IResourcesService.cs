﻿using Reservea.Microservices.Resources.Dtos.Requests;
using Reservea.Microservices.Resources.Dtos.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.Resources.Interfaces.Services
{
    public interface IResourcesService
    {
        Task<IEnumerable<ResourceForListResponse>> GetAllResourcesForListAsync(CancellationToken cancellationToken);
        Task<ResourceForDetailedResponse> GetResourceDetailsAsync(int resourceId, CancellationToken cancellationToken);
        Task<IEnumerable<ResourceAttributeForDetailedResourceResponse>> GetResourceAttributesForTypeChange(int resourceId, int resourceTypeId, CancellationToken cancellationToken);
        Task UpdateResourceAsync(int resourceId, UpdateResourceRequest request, CancellationToken cancellationToken);
        Task<IEnumerable<ResourceWithAvaiabilityResponse>> GetResourcesAvailabilityAsync(int resourceTypeId, CancellationToken cancellationToken);
        Task<AddResourceResponse> AddResourceAsync(AddResourceRequest request, CancellationToken cancellationToken);
        Task RemoveResourceAsync(int resourceId, CancellationToken cancellationToken);
        Task<ResourceAvailabilityResponse> GetResourceAvailabilityAsync(int resourceId, CancellationToken cancellationToken);
        Task<bool> ValidateAsync(IEnumerable<ReservationValidationRequest> reservations, CancellationToken cancellationToken);
    }
}
