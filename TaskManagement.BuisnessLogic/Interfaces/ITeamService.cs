using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.BuisnessLogic.Contracts.Teams.Commands;
using TaskManagement.DataAccess.Constants;
using TaskManagement.DataAccess.Dtos;

namespace TaskManagement.DataAccess.Interfaces
{
    public interface ITeamService
    {
        Task<Result<TeamDto>> CreateTeam(CreateTeamCommand teamDto , CancellationToken cancellationToken);
        Task<bool> DeleteTeam(int TeamId,CancellationToken cancellationToken);
        Task<TeamDto> GetTeam(int TeamId , CancellationToken cancellationToken);
        Task<List<TeamDto>> GetAllTeams(CancellationToken cancellationToken);
        Task<bool> AssignTaskToTeam(int TeamId, int TaskId , CancellationToken cancellationToken);
    }
}
