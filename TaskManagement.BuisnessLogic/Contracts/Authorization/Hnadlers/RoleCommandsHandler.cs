using MediatR;
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
using TaskManagement.DataAccess.Errors;

namespace TaskManagement.BuisnessLogic.Contracts.Authorization.Hnadlers
{
    public class RoleCommandsHandler(IRoleService roleService,RoleManager<IdentityRole> roleManager) : IRequestHandler<AddRoleCommand, Result<RoleResponse>>,
        IRequestHandler<UpdateRoleCommand,Result>
    {
        private readonly IRoleService roleService = roleService;
        private readonly RoleManager<IdentityRole> roleManager = roleManager;

        public async Task<Result<RoleResponse>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var roleExists = await roleManager.RoleExistsAsync(request.Name);
            if (roleExists)
                return Result.Failure<RoleResponse>(RoleErrors.DuplicatedRole);

            return await roleService.AddRoleAsync(request, cancellationToken);
        }

        public async Task<Result> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var roleExists = await roleManager.Roles.AnyAsync(x => x.Name == request.Name && x.Id != request.RoleId);
            if (roleExists)
                return Result.Failure(RoleErrors.DuplicatedRole);

            var role = await roleManager.FindByIdAsync(request.RoleId);
            if (role == null)
                return Result.Failure(RoleErrors.RoleNotFound);
            return await roleService.UpdateRoleAsync(request.RoleId, request, cancellationToken);
        }
    }
}
