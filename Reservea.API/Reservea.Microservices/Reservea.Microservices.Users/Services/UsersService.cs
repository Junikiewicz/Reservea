using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Reservea.Microservices.Users.Dtos.Requests;
using Reservea.Microservices.Users.Dtos.Responses;
using Reservea.Microservices.Users.Interfaces.Services;
using Reservea.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Reservea.Microservices.Users.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public UsersService(UserManager<User> userManager, RoleManager<Role> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserForListResponse>> GetUsersAsync(CancellationToken cancellationToken)
        {
            return await _userManager.Users.Select(x => new UserForListResponse
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
                Roles = x.UserRoles.Select(y=> new RoleResponse { Id=y.RoleId, Name=y.Role.Name })
            }).SingleAsync(cancellationToken);
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
