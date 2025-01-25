using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.BuisnessLogic.Contracts.Authorization.Commands;
using TaskManagement.BuisnessLogic.Interfaces;
using TaskManagement.DataAccess.Constants;
using TaskManagement.DataAccess.Dtos;

namespace TaskManagement.BuisnessLogic.Contracts.Authorization.Hnadlers
{
    public class RoleCommandsHandler(IRoleService roleService) : IRequestHandler<AddRoleCommand, Result<RoleResponse>>,
        IRequestHandler<UpdateRoleCommand,Result>
    {
        private readonly IRoleService roleService = roleService;

        public async Task<Result<RoleResponse>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            return await roleService.AddRoleAsync(request, cancellationToken); 
        }

        public async Task<Result> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            return await roleService.UpdateRoleAsync(request.RoleId,request, cancellationToken);
        }
    }
}
