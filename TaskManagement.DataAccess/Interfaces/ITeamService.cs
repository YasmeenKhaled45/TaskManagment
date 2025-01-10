using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccess.Dtos.Teams;

namespace TaskManagement.DataAccess.Interfaces
{
    public interface ITeamService
    {
        Task<TeamDto> CreateTeam(CreateTeamDto teamDto , CancellationToken cancellationToken);
        Task<bool> DeleteTeam(int TeamId,CancellationToken cancellationToken);
        Task<TeamDto> GetTeam(int TeamId , CancellationToken cancellationToken);
        Task<List<TeamDto>> GetAllTeams(CancellationToken cancellationToken);
        Task<bool> AssignTaskToTeam(int TeamId, int TaskId , CancellationToken cancellationToken);
    }
}
