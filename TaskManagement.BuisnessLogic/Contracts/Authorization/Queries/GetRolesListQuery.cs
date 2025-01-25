using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccess.Dtos;

namespace TaskManagement.BuisnessLogic.Contracts.Authorization.Queries
{
    public class GetRolesListQuery : IRequest<IEnumerable<RoleResponse>>
    {
    }
}
