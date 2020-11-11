using Microsoft.AspNetCore.Mvc;
using Reservea.Microservices.Resources.Dtos.Requests;
using Reservea.Microservices.Resources.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.Resources.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributesController : ControllerBase
    {
        private readonly IAttributesService _attributesService;

        public AttributesController(IAttributesService attributesService)
        {
            _attributesService = attributesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAttributesForListAsync(CancellationToken cancellationToken)
        {
            var response = await _attributesService.GetAllAttributesForListAsync(cancellationToken);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddAttributeAsync(AddAttributeRequest request, CancellationToken cancellationToken)
        {
            var result = await _attributesService.AddAttributeAsync(request, cancellationToken);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAttributeAsync(int id, UpdateAttributeRequest request, CancellationToken cancellationToken)
        {
            await _attributesService.UpdateAttributeAsync(id, request, cancellationToken);
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAttributeAsync(int id, CancellationToken cancellationToken)
        {
            await _attributesService.RemoveAttributeAsync(id, cancellationToken);

            return NoContent();
        }
    }
}
