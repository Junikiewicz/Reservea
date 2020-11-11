using Microsoft.AspNetCore.Mvc;
using Reservea.Microservices.Resources.Dtos.Requests;
using Reservea.Microservices.Resources.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.Resources.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceTypesController : ControllerBase
    {
        private readonly IResourceTypesService _resourceTypesService;

        public ResourceTypesController(IResourceTypesService resourceTypesService)
        {
            _resourceTypesService = resourceTypesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllResourceTypesForListAsync(CancellationToken cancellationToken)
        {
            var response = await _resourceTypesService.GetAllResourceTypesForListAsync(cancellationToken);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetResourceTypeDetailsAsync(int id, CancellationToken cancellationToken)
        {
            var response = await _resourceTypesService.GetResourceTypeDetailsAsync(id,cancellationToken);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddResourceTypeAsync(AddResourceTypeRequest request, CancellationToken cancellationToken)
        {
            var result = await _resourceTypesService.AddResourceTypeAsync(request, cancellationToken);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResourceTypeAsync(int id, UpdateResourceTypeRequest request, CancellationToken cancellationToken)
        {
            await _resourceTypesService.UpdateResourceTypeAsync(id, request, cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResourceTypeAsync(int id, CancellationToken cancellationToken)
        {
            await _resourceTypesService.DeleteResourceTypeAsync(id, cancellationToken);

            return NoContent();
        }

        [HttpPut("{id}/attributes")]
        public async Task<IActionResult> UpdateResourceTypeAttributesAsync(int id, UpdateResourceTypeAttributesRequest request, CancellationToken cancellationToken)
        {
            await _resourceTypesService.UpdateResourceTypeAttributesAsync(id, request, cancellationToken);

            return NoContent();
        }
    }
}
