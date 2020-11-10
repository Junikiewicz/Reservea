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
        public async Task<IActionResult> GetAllResourceTypesForList(CancellationToken cancellationToken)
        {
            var response = await _resourceTypesService.GetAllResourceTypesForListAsync(cancellationToken);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetResourceTypeDetails(int id, CancellationToken cancellationToken)
        {
            var response = await _resourceTypesService.GetResourceTypeDetailsByIdAsync(id,cancellationToken);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddResourceType(AddResourceTypeRequest request, CancellationToken cancellationToken)
        {
            var result = await _resourceTypesService.AddResourceTypeAsync(request, cancellationToken);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResourceType(int id, UpdateResourceTypeRequest request, CancellationToken cancellationToken)
        {
            await _resourceTypesService.UpdateResourceTypeAsync(id, request, cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResourceType(int id, CancellationToken cancellationToken)
        {
            await _resourceTypesService.DeleteResourceTypeAsync(id, cancellationToken);

            return NoContent();
        }
    }
}
