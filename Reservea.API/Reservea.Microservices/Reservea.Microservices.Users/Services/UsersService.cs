using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Reservea.Microservices.Users.Dtos.Requests;
using Reservea.Microservices.Users.Dtos.Responses;
using Reservea.Microservices.Users.Interfaces.Services;
using Reservea.Persistance.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.Users.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UsersService(UserManager<User> userManager, RoleManager<Role> roleManager, IMapper mapper, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _configuration = configuration;
            _configuration = configuration;
        }

        public async Task<IEnumerable<UserForListResponse>> GetUsersAsync(CancellationToken cancellationToken)
        {
            return await _userManager.Users.Where(x=>x.IsActive).Select(x => new UserForListResponse
            {
                Id = x.Id,
                Email = x.Email,
                IsActive = x.IsActive,
                Roles = x.UserRoles.Select(x => x.RoleId)
            }).ToListAsync(cancellationToken);
        }

        public async Task<UserForDetailedResponse> GetUserDetailsAsync(int userId, CancellationToken cancellationToken)
        {
            return await _userManager.Users.Where(x => x.Id == userId).Select(x => new UserForDetailedResponse
            {
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                IsActive = x.IsActive,
                Roles = x.UserRoles.Select(y => new RoleResponse { Id = y.RoleId, Name = y.Role.Name })
            }).SingleAsync(cancellationToken);
        }

        public async Task DeleteUserAsync(int userId, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.SingleAsync(x => x.Id == userId, cancellationToken);

            user.FirstName = Encrypt(user.FirstName);
            user.LastName = Encrypt(user.LastName);
            user.Email = Encrypt(user.Email);
            user.UserName = Encrypt(user.UserName);
            user.IsActive = false;

            await _userManager.UpdateAsync(user);
        }

        private string Encrypt(string input)
        {
            string output;
            var clearBytes = Encoding.Unicode.GetBytes(input);
            using (var encryptor = Aes.Create())
            {
                var pdb = new Rfc2898DeriveBytes(_configuration["EncryptionKey"], new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    output = Convert.ToBase64String(ms.ToArray());
                }
            }

            return output;
        }

        public async Task UpdateUserAsync(int userId, UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.SingleAsync(x => x.Id == userId, cancellationToken);
            var userRoles = await _userManager.GetRolesAsync(user) ?? Array.Empty<string>();

            request.Roles ??= Array.Empty<string>();

            await _userManager.AddToRolesAsync(user, request.Roles.Except(userRoles));
            await _userManager.RemoveFromRolesAsync(user, userRoles.Except(request.Roles));

            _mapper.Map(request, user);

            await _userManager.UpdateAsync(user);
        }
    }
}
