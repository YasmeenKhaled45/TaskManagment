using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccess.Constants;
using TaskManagement.DataAccess.Dtos;

namespace TaskManagement.BuisnessLogic.Contracts.Authorization.Commands
{
    public class AddRoleCommand : IRequest<Result<RoleResponse>>
    {
        public string Name { get; set; }
    }
}
