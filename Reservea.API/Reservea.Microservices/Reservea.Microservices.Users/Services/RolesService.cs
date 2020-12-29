using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    public class RolesService : IRolesService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public RolesService(UserManager<User> userManager, RoleManager<Role> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleResponse>> GetRolesAsync(CancellationToken cancellationToken)
        {
            return await _mapper.ProjectTo<RoleResponse>(_roleManager.Roles).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<RoleResponse>> GetUserRolesAsync(int userId, CancellationToken cancellationToken)
        {
            var query = _userManager.Users
                .Where(x => x.Id == userId)
                .Select(u => u.UserRoles.Select(ur => new RoleResponse { Id = ur.RoleId, Name = ur.Role.Name }));

            return await query.SingleAsync(cancellationToken);
        }

        public async Task AddRoleAsync(string roleName)
        {
            await _roleManager.CreateAsync(new Role { Name = roleName });
        }

        public async Task UpdateUserRoles(int userId, IEnumerable<string> rolesToAdd)
        {
            rolesToAdd ??= Array.Empty<string>();

            var user = await _userManager.FindByIdAsync(userId.ToString());         
            var userRoles = await _userManager.GetRolesAsync(user);

            await _userManager.AddToRolesAsync(user, rolesToAdd.Except(userRoles));
            await _userManager.RemoveFromRolesAsync(user, userRoles.Except(rolesToAdd));
        }
    }
}