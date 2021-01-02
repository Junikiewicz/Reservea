using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reservea.Microservices.Users.Dtos.Requests;
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
        /// Na podstawie dostarczonych przez użytkownika danych do logowania generuje token JWT
        /// </summary>
        /// <remarks></remarks>
        /// <param name="loginRequest">Email i hasło</param>
        /// <returns>Token JWT</returns>
        /// <response code="200">Logowanie powiodło sie</response>
        /// <response code="401">Nieprawidłowy email lub hasło</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var jwtToken = await _authenticationService.Login(loginRequest.Email, loginRequest.Password);

            return Ok(jwtToken);
        }

        /// <summary>
        /// Tworzy nowego użytkownika
        /// </summary>
        /// <remarks></remarks>
        /// <param name="registerRequest">Email, hasło oraz dane osobowe</param>
        /// <returns></returns>
        /// <response code="201">Utworzenie nowego użytkownika powiodło się</response>
        /// <response code="400">Nie udało się stworzyć użytkownika o podanych danych</response>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            await _authenticationService.Register(registerRequest.Email, registerRequest.Password, registerRequest.FirstName, registerRequest.LastName);

            return NoContent();
        }

        [HttpPost("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailRequest confirmEmailRequest)
        {
            await _authenticationService.ConfirmEmail(confirmEmailRequest);

            return NoContent();
        }
    }
}