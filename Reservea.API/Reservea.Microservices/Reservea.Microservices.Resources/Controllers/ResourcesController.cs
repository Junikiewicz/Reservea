using Microsoft.AspNetCore.Mvc;
using Reservea.Microservices.Resources.Dtos.Requests;
using Reservea.Microservices.Resources.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.Resources.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly IResourcesService _resourcesService;

        public ResourcesController(IResourcesService resourcesService)
        {
            _resourcesService = resourcesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllResourcesForList(CancellationToken cancellationToken)
        {
            var resourcesForList = await _resourcesService.GetAllResourcesForListAsync(cancellationToken);

            return Ok(resourcesForList);
        }

        [HttpGet("{resourceId}")]
        public async Task<IActionResult> GetResourceDetails(int resourceId, CancellationToken cancellationToken)
        {
            var resourceDetails = await _resourcesService.GetResourceDetailsByIdAsync(resourceId, cancellationToken);

            return Ok(resourceDetails);
        }

        [HttpPost]
        public async Task<IActionResult> AddResource(AddResourceRequest request, CancellationToken cancellationToken)
        {
            var result = await _resourcesService.AddResourceAsync(request, cancellationToken);

            return Ok(result);
        }

        [HttpPatch("{resourceId}")]
        public async Task<IActionResult> UpdateResourceAttributes(int resourceId, UpdateResourceAttributesRequest request, CancellationToken cancellationToken)
        {
            await _resourcesService.UpdateResourceAttributesAsync(resourceId, request, cancellationToken);

            return NoContent();
        }
    }
}
