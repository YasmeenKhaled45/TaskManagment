using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccess.Constants;
using TaskManagement.DataAccess.Dtos;

namespace TaskManagement.BuisnessLogic.Contracts.Teams.Commands
{
    public class CreateTeamCommand : IRequest<Result<TeamDto>>
    {
        public string Name { get; set; } = string.Empty;
       public List<string> UserIds { get; set; } = new List<string>();
    }
}
