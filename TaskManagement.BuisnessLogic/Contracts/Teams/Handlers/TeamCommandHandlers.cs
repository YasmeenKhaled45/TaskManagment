using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.BuisnessLogic.Contracts.Teams.Commands;
using TaskManagement.DataAccess.Constants;
using TaskManagement.DataAccess.Data;
using TaskManagement.DataAccess.Dtos;
using TaskManagement.DataAccess.Entities;
using TaskManagement.DataAccess.Interfaces;

namespace TaskManagement.BuisnessLogic.Contracts.Teams.Handlers
{
    public class TeamCommandHandlers(ITeamService teamService,AppDbContext context) : IRequestHandler<CreateTeamCommand, Result<TeamDto>>
    {
        private readonly ITeamService teamService = teamService;
        private readonly AppDbContext context = context;

        public  async Task<Result<TeamDto>> Handle(CreateTeamCommand teamCommand, CancellationToken cancellationToken)
        {
            var team = teamCommand.Adapt<Team>();
            foreach (var userId in teamCommand.UserIds)
            {
                var user = await context.Users.FindAsync(userId);
                if (user != null)
                {
                    team.TeamUsers.Add(new TeamUser { UserId = user.Id });
                }
            }
            return await teamService.CreateTeam(team, cancellationToken);
        }
    }
}
