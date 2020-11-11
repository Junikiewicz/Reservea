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
        public async Task<IActionResult> GetAllResourcesForListAsync(CancellationToken cancellationToken)
        {
            var resourcesForList = await _resourcesService.GetAllResourcesForListAsync(cancellationToken);

            return Ok(resourcesForList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetResourceDetailsAsync(int id, CancellationToken cancellationToken)
        {
            var resourceDetails = await _resourcesService.GetResourceDetailsAsync(id, cancellationToken);

            return Ok(resourceDetails);
        }

        [HttpPost]
        public async Task<IActionResult> AddResourceAsync(AddResourceRequest request, CancellationToken cancellationToken)
        {
            var result = await _resourcesService.AddResourceAsync(request, cancellationToken);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResourceAsync(int id, UpdateResourceRequest request, CancellationToken cancellationToken)
        {
            await _resourcesService.UpdateResourceAsync(id, request, cancellationToken);

            return NoContent();
        }

        [HttpPut("{id}/attributes")]
        public async Task<IActionResult> UpdateResourceAttributesAsync(int id, UpdateResourceAttributesRequest request, CancellationToken cancellationToken)
        {
            await _resourcesService.UpdateResourceAttributesAsync(id, request, cancellationToken);

            return NoContent();
        }
    }
}
