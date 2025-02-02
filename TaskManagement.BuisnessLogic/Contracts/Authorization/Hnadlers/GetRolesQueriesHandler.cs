using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.BuisnessLogic.Contracts.Authorization.Queries;
using TaskManagement.BuisnessLogic.Interfaces;
using TaskManagement.DataAccess.Dtos;

namespace TaskManagement.BuisnessLogic.Contracts.Authorization.Hnadlers
{
    public class GetRolesQueriesHandler(IRoleService roleService) : IRequestHandler<GetRolesListQuery, IEnumerable<RoleResponse>>
    {
        private readonly IRoleService roleService = roleService;

        public async Task<IEnumerable<RoleResponse>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
        {
            var roles = await roleService.GetAllRoles(cancellationToken);
            return roles.Where(r => r.Name != "User");
        }
    }
}
