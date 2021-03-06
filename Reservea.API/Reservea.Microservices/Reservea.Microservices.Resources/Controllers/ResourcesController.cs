﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reservea.Microservices.Resources.Dtos.Requests;
using Reservea.Microservices.Resources.Interfaces.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.Resources.Controllers
{
    [Authorize]
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
        [Authorize(Roles = "Admin,Employee")]
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
        [Authorize(Roles = "Admin,Employee")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetResourceDetailsAsync(int id, CancellationToken cancellationToken)
        {
            var resourceDetails = await _resourcesService.GetResourceDetailsAsync(id, cancellationToken);

            return Ok(resourceDetails);
        }

        /// <summary>
        /// Pobiera informacje na temat dostępności danego typu zasobu
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="id">Identyfikator zasobu</param>
        /// <param name="cancellationToken">Token umożliwiający przerwanie wykonywania rządania</param>
        /// <returns>Lista definiująca dostępność danego zasobu</returns>
        /// <response code="200">Pobranie danych powiodło się</response>
        [AllowAnonymous]
        [HttpGet("availability")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetResourcesTypeAvailabilityAsync([FromQuery]int resourceTypeId, CancellationToken cancellationToken)
        {
            var resourceDetails = await _resourcesService.GetResourcesAvailabilityAsync(resourceTypeId, cancellationToken);

            return Ok(resourceDetails);
        }

        /// <summary>
        /// Pobiera informacje na temat dostępności danego zasobu
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="id">Identyfikator zasobu</param>
        /// <param name="cancellationToken">Token umożliwiający przerwanie wykonywania rządania</param>
        /// <returns>Lista definiująca dostępność danego zasobu</returns>
        /// <response code="200">Pobranie danych powiodło się</response>
        [Authorize(Roles = "Admin,Employee")]
        [HttpGet("{id}/availability")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetResourceAvailabilityAsync(int id, CancellationToken cancellationToken)
        {
            var resourceDetails = await _resourcesService.GetResourceAvailabilityAsync(id, cancellationToken);

            return Ok(resourceDetails);
        }

        /// <summary>
        /// Pobiera liste atrybutów do zmiany typu
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="resourceId">Identyfikator zasobu</param>
        /// <param name="resourceTypeId">Identyfikator nowego typu zasobu</param>
        /// <param name="cancellationToken">Token umożliwiający przerwanie wykonywania rządania</param>
        /// <returns>Szczegółowe dane zasobu zdefiniowanego w systemie</returns>
        /// <response code="200">Pobranie danych powiodło się</response>
        [Authorize(Roles = "Admin,Employee")]
        [HttpGet("{resourceId}/{resourceTypeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetResourceAttributesForTypeChange(int resourceId, int resourceTypeId, CancellationToken cancellationToken)
        {
            var attributes = await _resourcesService.GetResourceAttributesForTypeChange(resourceId, resourceTypeId, cancellationToken);

            return Ok(attributes);
        }


        /// <summary>
        /// Waliduje czy zasób jest dostępny w podanych ramach
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="reservations"></param>
        /// <param name="cancellationToken">Token umożliwiający przerwanie wykonywania rządania</param>
        /// <returns>Szczegółowe dane nowo utworzonego zasobu</returns>
        /// <response code="200">Dodanie zasobu powiodło się</response>
        [AllowAnonymous]
        [HttpPost("validate-avaiability")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ValidateAvaiability(IEnumerable<ReservationValidationRequest> reservations, CancellationToken cancellationToken)
        {
            var result = await _resourcesService.ValidateAsync(reservations, cancellationToken);

            return Ok(result);
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
        [Authorize(Roles = "Admin,Employee")]
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
        [Authorize(Roles = "Admin,Employee")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateResourceAsync(int id, UpdateResourceRequest request, CancellationToken cancellationToken)
        {
            await _resourcesService.UpdateResourceAsync(id, request, cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Oznacza zasób jako usunięty
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="id">Identyfikator zasobu do usunięcia</param>
        /// <param name="cancellationToken">Token umożliwiający przerwanie wykonywania rządania</param>
        /// <returns></returns>
        /// <response code="204">Zasób został oznaczony jako usunięty</response>
        [Authorize(Roles = "Admin,Employee")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteResourceAsync(int id, CancellationToken cancellationToken)
        {
            await _resourcesService.RemoveResourceAsync(id, cancellationToken);

            return NoContent();
        }
    }
}
