using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccess.Constants;

namespace TaskManagement.BuisnessLogic.Contracts.Authorization.Commands
{
    public class UpdateRoleCommand : IRequest<Result>
    {
        public string RoleId { get; set; }
        public string Name { get; set; }
    }
}
