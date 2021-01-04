using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reservea.Microservices.Resources.Dtos.Requests;
using Reservea.Microservices.Resources.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.Resources.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AttributesController : ControllerBase
    {
        private readonly IAttributesService _attributesService;

        public AttributesController(IAttributesService attributesService)
        {
            _attributesService = attributesService;
        }

        /// <summary>
        /// Pobiera wszystkie atrybuty zdefiniowane w systemie
        /// </summary>
        /// <remarks>
        /// Tu będzie informacja o paginacji, jak już ją wprowadze
        /// </remarks>
        /// <param name="cancellationToken">Token umożliwiający przerwanie wykonywania rządania</param>
        /// <returns>Lista wszystkich atrybutów zdefiniowanych w systemie</returns>
        /// <response code="200">Pobranie danych powiodło się</response>
        [Authorize(Roles = "Admin,Employee")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAttributesForListAsync(CancellationToken cancellationToken)
        {
            var response = await _attributesService.GetAllAttributesForListAsync(cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Tworzy nowy atrybut w systemie
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="request">Parametry nowego atrybutu</param>
        /// <param name="cancellationToken">Token umożliwiający przerwanie wykonywania rządania</param>
        /// <returns>Szczegółowe dane nowo utworzonego atrybutu</returns>
        /// <response code="200">Dodanie atrybutu powiodło się</response>
        [Authorize(Roles = "Admin,Employee")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAttributeAsync(AddAttributeRequest request, CancellationToken cancellationToken)
        {
            var result = await _attributesService.AddAttributeAsync(request, cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Aktualizuje parametry atrybutu znajdującego się w systemie
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="id">Identyfikator atrybutu do aktualizacji</param>
        /// <param name="request">Nowe parametry danego atrybutu</param>
        /// <param name="cancellationToken">Token umożliwiający przerwanie wykonywania rządania</param>
        /// <returns></returns>
        /// <response code="204">Atrybut został poprawnie zaaktualizowany</response>
        [Authorize(Roles = "Admin,Employee")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateAttributeAsync(int id, UpdateAttributeRequest request, CancellationToken cancellationToken)
        {
            await _attributesService.UpdateAttributeAsync(id, request, cancellationToken);
            
            return NoContent();
        }

        /// <summary>
        /// Usuwa atrybut z systemu
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="id">Identyfikator atrybutu do usunięcia</param>
        /// <param name="cancellationToken">Token umożliwiający przerwanie wykonywania rządania</param>
        /// <returns></returns>
        /// <response code="204">Atrybut został poprawnie usunięty</response>
        [Authorize(Roles = "Admin,Employee")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RemoveAttributeAsync(int id, CancellationToken cancellationToken)
        {
            await _attributesService.RemoveAttributeAsync(id, cancellationToken);

            return NoContent();
        }
    }
}
