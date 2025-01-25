using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.BuisnessLogic.Contracts.Authorization.Commands;
using TaskManagement.DataAccess.Constants;
using TaskManagement.DataAccess.Dtos;

namespace TaskManagement.BuisnessLogic.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleResponse>> GetAllRoles(CancellationToken cancellationToken);
        Task<Result> UpdateRoleAsync(string RoleId,UpdateRoleCommand roleCommand,CancellationToken cancellationToken);
        Task<Result<RoleResponse>> AddRoleAsync(AddRoleCommand addRole, CancellationToken cancellationToken);
    }
}
