using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reservea.Microservices.CMS.Dtos.Requests;
using Reservea.Microservices.CMS.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.CMS.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotosService _photosService;

        public PhotosController(IPhotosService photosService)
        {
            _photosService = photosService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllPhotos(CancellationToken cancellationToken)
        {
            return Ok(await _photosService.GetPhotos(cancellationToken));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhoto(int id, CancellationToken cancellationToken)
        {
            return Ok(await _photosService.GetPhoto(id, cancellationToken));
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int id, CancellationToken cancellationToken)
        {
            await _photosService.DeletePhoto(id, cancellationToken);

            return NoContent();
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] AddPhotoRequest request, CancellationToken cancellationToken)
        {
            var response = await _photosService.AddPhoto(request, cancellationToken);

            return Ok(response);
        }
    }
}