using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Pobiera wszystkie typy zasobów zdefiniowane w systemie
        /// </summary>
        /// <remarks>
        /// Tu będzie informacja o paginacji, jak już ją wprowadze
        /// </remarks>
        /// <param name="cancellationToken">Token umożliwiający przerwanie wykonywania rządania</param>
        /// <returns>Lista wszystkich typów zasobów zdefiniowanych w systemie</returns>
        /// <response code="200">Pobranie danych powiodło się</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllResourceTypesForListAsync(CancellationToken cancellationToken)
        {
            var response = await _resourceTypesService.GetAllResourceTypesForListAsync(cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Pobiera wszystkie typy zasobów zdefiniowane w systemie, wraz z ich opisami
        /// </summary>
        /// <remarks>
        /// Tu będzie informacja o paginacji, jak już ją wprowadze
        /// </remarks>
        /// <param name="cancellationToken">Token umożliwiający przerwanie wykonywania rządania</param>
        /// <returns>Lista wszystkich typów zasobów zdefiniowanych w systemie, wraz z ich opisami</returns>
        /// <response code="200">Pobranie danych powiodło się</response>
        [HttpGet("details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllResourceTypesWithDetailsForListAsync(CancellationToken cancellationToken)
        {
            var response = await _resourceTypesService.GetAllResourceTypesWithDetailsForListAsync(cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Pobiera szczegółowe informacje na temat danego typu zasobu
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="id">Identyfikator typu zasobu</param>
        /// <param name="cancellationToken">Token umożliwiający przerwanie wykonywania rządania</param>
        /// <returns>Szczegółowe dane typu zasobu zdefiniowanego w systemie</returns>
        /// <response code="200">Pobranie danych powiodło się</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetResourceTypeDetailsAsync(int id, CancellationToken cancellationToken)
        {
            var response = await _resourceTypesService.GetResourceTypeDetailsAsync(id,cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Pobiera listę atrybotów przypisaną do danego typu zasobu
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="id">Identyfikator typu zasobu</param>
        /// <param name="cancellationToken">Token umożliwiający przerwanie wykonywania rządania</param>
        /// <returns>Lista atrybutów przypisanych do danego typu zasobu</returns>
        /// <response code="200">Pobranie danych powiodło się</response>
        [HttpGet("{id}/attributes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetResourceTypeAttributesAsync(int id, CancellationToken cancellationToken)
        {
            var response = await _resourceTypesService.GetResourceTypeAttributesAsync(id, cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Tworzy nowy typ zasobu w systemie
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="request">Parametry nowego typu zasobu</param>
        /// <param name="cancellationToken">Token umożliwiający przerwanie wykonywania rządania</param>
        /// <returns>Szczegółowe dane nowo utworzonego typu zasobu</returns>
        /// <response code="200">Dodanie typu zasobu powiodło się</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddResourceTypeAsync(AddResourceTypeRequest request, CancellationToken cancellationToken)
        {
            var result = await _resourceTypesService.AddResourceTypeAsync(request, cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Aktualizuje parametry typu zasobu znajdującego się w systemie
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="id">Identyfikator zasobu do aktualizacji</param>
        /// <param name="request">Nowe parametry danego zasobu</param>
        /// <param name="cancellationToken">Token umożliwiający przerwanie wykonywania rządania</param>
        /// <returns></returns>
        /// <response code="204">Typ zasobu został poprawnie zaaktualizowany</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateResourceTypeAsync(int id, UpdateResourceTypeRequest request, CancellationToken cancellationToken)
        {
            await _resourceTypesService.UpdateResourceTypeAsync(id, request, cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Usuwa typ zasobu z systemu
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="id">Identyfikator typu zasobu do usunięcia</param>
        /// <param name="cancellationToken">Token umożliwiający przerwanie wykonywania rządania</param>
        /// <returns></returns>
        /// <response code="204">Typ zasobu został poprawnie usunięty</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RemoveResourceTypeAsync(int id, CancellationToken cancellationToken)
        {
            await _resourceTypesService.RemoveResourceTypeAsync(id, cancellationToken);

            return NoContent();
        }
    }
}
