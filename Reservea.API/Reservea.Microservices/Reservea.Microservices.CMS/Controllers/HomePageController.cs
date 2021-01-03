using Microsoft.AspNetCore.Mvc;
using Reservea.Microservices.CMS.Dtos.Requests;
using Reservea.Microservices.CMS.Interfaces.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomePageController : ControllerBase
    {
        private readonly IHomePageService _homePageService;

        public HomePageController(IHomePageService homePageService)
        {
            _homePageService = homePageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTextFieldsContents(CancellationToken cancellationToken)
        {
            return Ok(await _homePageService.GetAllTextFieldsContents(cancellationToken));
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetTextFieldContent(string name, CancellationToken cancellationToken)
        {
            return Ok(await _homePageService.GetTextFieldContent(name, cancellationToken));
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateTextFieldContent(IEnumerable<UpdateTextFieldContentRequest> request, CancellationToken cancellationToken)
        {
            await _homePageService.UpdateTextFieldContent(request, cancellationToken);

            return NoContent();
        }
    }
}
