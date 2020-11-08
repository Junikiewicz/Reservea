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

        [HttpPost]
        public async Task<IActionResult> AddResource(AddResourceRequest request, CancellationToken cancellationToken)
        {
            await _resourcesService.AddResource(request, cancellationToken);

            return NoContent();
        }
    }
}
