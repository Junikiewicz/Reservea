using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reservea.Microservices.CMS.Dtos.Requests;
using Reservea.Microservices.CMS.Interfaces.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.CMS.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HomePageController : ControllerBase
    {
        private readonly IHomePageService _homePageService;

        public HomePageController(IHomePageService homePageService)
        {
            _homePageService = homePageService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllTextFieldsContents(CancellationToken cancellationToken)
        {
            return Ok(await _homePageService.GetAllTextFieldsContents(cancellationToken));
        }

        [AllowAnonymous]
        [HttpGet("{name}")]
        public async Task<IActionResult> GetTextFieldContent(string name, CancellationToken cancellationToken)
        {
            return Ok(await _homePageService.GetTextFieldContent(name, cancellationToken));
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpPatch]
        public async Task<IActionResult> UpdateTextFieldContent(IEnumerable<UpdateTextFieldContentRequest> request, CancellationToken cancellationToken)
        {
            await _homePageService.UpdateTextFieldContent(request, cancellationToken);

            return NoContent();
        }
    }
}
