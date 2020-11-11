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
    public class ResourcesController : ControllerBase
    {
        private readonly IResourcesService _resourcesService;

        public ResourcesController(IResourcesService resourcesService)
        {
            _resourcesService = resourcesService;
        }

        /// <summary>
        /// Pobiera wszystkie zasoby zdefiniowane w systemie
        /// </summary>
        /// <remarks>
        /// Tu będzie informacja o paginacji, jak już ją wprowadze
        /// </remarks>
        /// <param name="cancellationToken">Token umożliwiający przerwanie wykonywania rządania</param>
        /// <returns>Lista wszystkich zasobów zdefiniowanych w systemie</returns>
        /// <response code="200">Pobranie danych powiodło się</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllResourcesForListAsync(CancellationToken cancellationToken)
        {
            var resourcesForList = await _resourcesService.GetAllResourcesForListAsync(cancellationToken);

            return Ok(resourcesForList);
        }

        /// <summary>
        /// Pobiera szczegółowe informacje na temat danego zasobu
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="id">Identyfikator zasobu</param>
        /// <param name="cancellationToken">Token umożliwiający przerwanie wykonywania rządania</param>
        /// <returns>Szczegółowe dane zasobu zdefiniowanego w systemie</returns>
        /// <response code="200">Pobranie danych powiodło się</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetResourceDetailsAsync(int id, CancellationToken cancellationToken)
        {
            var resourceDetails = await _resourcesService.GetResourceDetailsAsync(id, cancellationToken);

            return Ok(resourceDetails);
        }

        /// <summary>
        /// Tworzy nowy zasób w systemie
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="request">Parametry nowego zasobu</param>
        /// <param name="cancellationToken">Token umożliwiający przerwanie wykonywania rządania</param>
        /// <returns>Szczegółowe dane nowo utworzonego zasobu</returns>
        /// <response code="200">Dodanie zasobu powiodło się</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddResourceAsync(AddResourceRequest request, CancellationToken cancellationToken)
        {
            var result = await _resourcesService.AddResourceAsync(request, cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Aktualizuje parametry zasobu znajdującego się w systemie
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="id">Identyfikator zasobu do aktualizacji</param>
        /// <param name="request">Nowe parametry danego zasobu</param>
        /// <param name="cancellationToken">Token umożliwiający przerwanie wykonywania rządania</param>
        /// <returns></returns>
        /// <response code="204">Zasób został poprawnie zaaktualizowany</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateResourceAsync(int id, UpdateResourceRequest request, CancellationToken cancellationToken)
        {
            await _resourcesService.UpdateResourceAsync(id, request, cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Aktualizuje atrybuty przypisaną do danego zasobu
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="id">Identyfikator zasobu którego atrybuty chcemy zaaktualizować</param>
        /// <param name="request">Dwie listy: atrybutów do usunięcia oraz atrybutów do dodania lub zaaktualizowania</param>
        /// <param name="cancellationToken">Token umożliwiający przerwanie wykonywania rządania</param>
        /// <returns></returns>
        /// <response code="204">Lista atrybutów została poprawnie zaaktualizowana</response>
        [HttpPut("{id}/attributes")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateResourceAttributesAsync(int id, UpdateResourceAttributesRequest request, CancellationToken cancellationToken)
        {
            await _resourcesService.UpdateResourceAttributesAsync(id, request, cancellationToken);

            return NoContent();
        }
    }
}
