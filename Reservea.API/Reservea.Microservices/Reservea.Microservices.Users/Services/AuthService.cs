using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Reservea.Common.Helpers;
using Reservea.Microservices.Users.Interfaces.Services;
using Reservea.Persistance.Users.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Reservea.Microservices.Users.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthService(IConfiguration configuration, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task Register(string email, string password, string firstName, string lastName)
        {
            User userToCreate = new User
            {
                UserName = email,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                IsActive = true
            };
            var userCreationResult = await _userManager.CreateAsync(userToCreate, password);

            if (!userCreationResult.Succeeded) throw new Exception(userCreationResult.Errors.ToString());
        }

        public async Task<string> Login(string email, string password)
        {
            var userFromDatabase = await _userManager.FindByEmailAsync(email);

            if (userFromDatabase == null || !userFromDatabase.IsActive) throw new AuthenticationException("Unauthorized");

            var signInResult = await _signInManager.CheckPasswordSignInAsync(userFromDatabase, password, false);

            if (!signInResult.Succeeded) throw new AuthenticationException();

            return await GenerateJwtToken(_configuration.GetSection("AppSettings:PrivateKey").Value, userFromDatabase);
        }

        private async Task<string> GenerateJwtToken(string privateKey, User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName)
            };

            var userRoles = await _userManager.GetRolesAsync(user);

            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = JwtTokenHelper.BuildRsaSigningKey(privateKey);
            var creds = new SigningCredentials(key, SecurityAlgorithms.RsaSha256Signature, SecurityAlgorithms.Sha256Digest);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(24),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
