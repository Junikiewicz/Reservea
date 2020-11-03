using Microsoft.AspNetCore.Mvc;
using Reservea.Microservices.Users.Dtos.Requests;
using Reservea.Microservices.Users.Dtos.Responses;
using Reservea.Microservices.Users.Interfaces.Services;
using System.Threading.Tasks;

namespace Reservea.Microservices.Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authenticationService;
        public AuthController(IAuthService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Provides user with jwt token used for authentication in the api
        /// </summary>
        /// <returns>Jwt token</returns>
        /// <param name="loginRequest">User email and password</param>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var token = await _authenticationService.Login(loginRequest.Email, loginRequest.Password);

            return Ok(new LoginResponse { JwtToken = token });
        }

        /// <summary>
        /// Creates new user
        /// </summary>
        /// <param name="registerRequest">Dto containg basic information about the user and his desired login credentials</param>
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            await _authenticationService.Register(registerRequest.Email, registerRequest.Password, registerRequest.FirstName, registerRequest.LastName);

            return NoContent();
        }
    }
}