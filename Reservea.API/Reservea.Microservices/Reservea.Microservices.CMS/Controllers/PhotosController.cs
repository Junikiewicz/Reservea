using Microsoft.AspNetCore.Mvc;
using Reservea.Microservices.CMS.Dtos.Requests;
using Reservea.Microservices.CMS.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotosService _photosService;

        public PhotosController(IPhotosService photosService)
        {
            _photosService = photosService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPhotos(CancellationToken cancellationToken)
        {
            return Ok(await _photosService.GetPhotos(cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhoto(int id, CancellationToken cancellationToken)
        {
            return Ok(await _photosService.GetPhoto(id, cancellationToken));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int id, CancellationToken cancellationToken)
        {
            await _photosService.DeletePhoto(id, cancellationToken);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] AddPhotoRequest request, CancellationToken cancellationToken)
        {
            var response = await _photosService.AddPhoto(request, cancellationToken);

            return Ok(response);
        }
    }
}