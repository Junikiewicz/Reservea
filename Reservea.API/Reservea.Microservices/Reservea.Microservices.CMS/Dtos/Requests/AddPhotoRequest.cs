using Microsoft.AspNetCore.Http;

namespace Reservea.Microservices.CMS.Dtos.Requests
{
    public class AddPhotoRequest
    {
        public IFormFile File { get; set; }
    }
}
