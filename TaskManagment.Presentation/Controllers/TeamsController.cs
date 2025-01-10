using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.BuisnessLogic.Services;
using TaskManagement.DataAccess.Dtos.Teams;
using TaskManagement.DataAccess.Interfaces;

namespace TaskManagment.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController(ITeamService teamService) : ControllerBase
    {
        private readonly ITeamService teamService = teamService;

        [HttpGet]
        public async Task<IActionResult> Allteams(CancellationToken cancellationToken)
        {
            var result = await teamService.GetAllTeams(cancellationToken);
            if(result is null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateTeam([FromBody] CreateTeamDto dto , CancellationToken cancellationToken)
        {
            var result = await teamService.CreateTeam(dto,cancellationToken);
            if (result == null)
                return BadRequest("Team creation failed.");
            return Ok(result);
        }
        [HttpPost("{TeamId}/AssignTask/{TaskId}")]
        public async Task<IActionResult> AssignTaskToTeam([FromRoute]int TeamId, [FromRoute]int TaskId,CancellationToken cancellationToken)
        {
            var success = await teamService.AssignTaskToTeam(TeamId, TaskId,cancellationToken);
            if (!success)
                return BadRequest("Assignment failed.");
            return Ok("Task assigned to team successfully.");
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Team([FromRoute] int Id, CancellationToken cancellationToken)
        {
            var res = await teamService.DeleteTeam(Id, cancellationToken);
            if (!res)
                return NotFound();
            return Ok("Done");
        }

    }
}
