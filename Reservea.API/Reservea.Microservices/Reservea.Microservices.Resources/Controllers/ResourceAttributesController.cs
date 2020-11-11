using Microsoft.AspNetCore.Mvc;
using Reservea.Microservices.Resources.Dtos.Requests;
using Reservea.Microservices.Resources.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.Resources.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceAttributesController : ControllerBase
    {
        private readonly IResourceAttributesService _resourcesService;

        public ResourceAttributesController(IResourceAttributesService resourceAttributesService)
        {
            _resourcesService = resourceAttributesService;
        }

        [HttpPut("{resourceId}")]
        public async Task<IActionResult> UpdateResourceAttributesAsync(int resourceId, UpdateResourceAttributesRequest request, CancellationToken cancellationToken)
        {
            await _resourcesService.UpdateResourceAttributesAsync(resourceId, request, cancellationToken);

            return NoContent();
        }
    }
}
