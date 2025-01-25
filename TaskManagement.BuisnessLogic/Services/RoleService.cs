using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.BuisnessLogic.Contracts.Authorization.Commands;
using TaskManagement.BuisnessLogic.Interfaces;
using TaskManagement.DataAccess.Constants;
using TaskManagement.DataAccess.Dtos;
using TaskManagement.DataAccess.Entities;
using TaskManagement.DataAccess.Errors;

namespace TaskManagement.BuisnessLogic.Services
{
    public class RoleService(RoleManager<IdentityRole> roleManager) : IRoleService
    {
        private readonly RoleManager<IdentityRole> roleManager = roleManager;

        public async Task<Result<RoleResponse>> AddRoleAsync(AddRoleCommand addRole, CancellationToken cancellationToken)
        {
            var roleExists = await roleManager.RoleExistsAsync(addRole.Name);
            if (roleExists)
                return Result.Failure<RoleResponse>(RoleErrors.DuplicatedRole);
            var role = new IdentityRole
            {
                Name = addRole.Name,
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            var result = await roleManager.CreateAsync(role);
            return Result.Success(role.Adapt<RoleResponse>());
        }

        public async Task<IEnumerable<RoleResponse>> GetAllRoles(CancellationToken cancellationToken)=> 
            await roleManager.Roles.Where(r=>r.Name != "User").ProjectToType<RoleResponse>().ToListAsync(cancellationToken);

        public async Task<Result> UpdateRoleAsync(string RoleId, UpdateRoleCommand roleCommand, CancellationToken cancellationToken)
        {
            var roleExists = await roleManager.Roles.AnyAsync(x => x.Name == roleCommand.Name && x.Id != RoleId);
            if (roleExists)
                return Result.Failure(RoleErrors.DuplicatedRole);
            if (await roleManager.FindByIdAsync(RoleId) is not { } role)
                return Result.Failure(RoleErrors.RoleNotFound);
            role.Name = roleCommand.Name;
            var result = await roleManager.UpdateAsync(role);
            if (result.Succeeded)
                return Result.Success();
            var error = result.Errors.First();
            return Result.Failure(new Error(error.Code, error.Description));
        }
    }
}
