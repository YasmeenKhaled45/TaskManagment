using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.BuisnessLogic.Contracts.Teams.Commands;
using TaskManagement.DataAccess.Constants;
using TaskManagement.DataAccess.Dtos;
using TaskManagement.DataAccess.Interfaces;

namespace TaskManagement.BuisnessLogic.Contracts.Teams.Handlers
{
    public class TeamCommandHandlers(ITeamService teamService) : IRequestHandler<CreateTeamCommand, Result<TeamDto>>
    {
        private readonly ITeamService teamService = teamService;

        public  async Task<Result<TeamDto>> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
           return await teamService.CreateTeam(request, cancellationToken);
        }
    }
}
