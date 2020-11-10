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
        public async Task<IActionResult> GetAllAttributesForList(CancellationToken cancellationToken)
        {
            var response = await _attributesService.GetAllAttributesForList(cancellationToken);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddAttribute(AddAttributeRequest request, CancellationToken cancellationToken)
        {
            var result = await _attributesService.AddAttribute(request, cancellationToken);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAttribute(int id, EditAttributeRequest request, CancellationToken cancellationToken)
        {
            await _attributesService.EditAttribute(id, request, cancellationToken);
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttribute(int id, CancellationToken cancellationToken)
        {
            await _attributesService.DeleteAttribute(id, cancellationToken);

            return NoContent();
        }
    }
}
