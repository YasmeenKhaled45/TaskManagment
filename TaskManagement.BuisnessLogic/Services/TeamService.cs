using Mapster;
using Microsoft.EntityFrameworkCore;
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

namespace TaskManagement.BuisnessLogic.Services
{
    public class TeamService(AppDbContext context) : ITeamService
    {
        private readonly AppDbContext context = context;

        public async Task<bool> AssignTaskToTeam(int TeamId, int TaskId, CancellationToken cancellationToken)
        {
            var team = await context.Teams.Include(x => x.Tasks).FirstOrDefaultAsync(x => x.TeamId == TeamId);
            var task = await context.Tasks.FindAsync(TaskId);
            if (team == null || task == null)  return false;

            team.Tasks.Add(task);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<Result<TeamDto>> CreateTeam(CreateTeamCommand teamCommand, CancellationToken cancellationToken)
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
            context.Teams.Add(team);
            await context.SaveChangesAsync(cancellationToken);
            return Result.Success(team.Adapt<TeamDto>());
        }

        public async Task<bool> DeleteTeam(int TeamId, CancellationToken cancellationToken)
        {
            var team = await context.Teams.Include(x=>x.Tasks).FirstOrDefaultAsync(x=>x.TeamId == TeamId);
            if (team == null) return false;

            foreach(var task in team.Tasks)
            {
                context.Tasks.Remove(task);
                await context.SaveChangesAsync(cancellationToken);
            }
            context.Teams.Remove(team);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<List<TeamDto>> GetAllTeams(CancellationToken cancellationToken)
        {
            var teams = await context.Teams.ToListAsync(cancellationToken);
            return teams.Adapt<List<TeamDto>>();
        }

        public async Task<TeamDto> GetTeam(int TeamId, CancellationToken cancellationToken)
        {
            var team = await context.Teams.FindAsync(TeamId, cancellationToken);
            return team.Adapt<TeamDto>();
        }
    }
}
