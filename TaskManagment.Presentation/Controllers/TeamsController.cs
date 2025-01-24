using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManagement.BuisnessLogic.Contracts.Teams.Commands;
using TaskManagement.BuisnessLogic.Services;
using TaskManagement.DataAccess.Interfaces;

namespace TaskManagment.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController(ITeamService teamService,IMediator mediator) : ControllerBase
    {
        private readonly ITeamService teamService = teamService;
        private readonly IMediator mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> Allteams(CancellationToken cancellationToken)
        {
            var result = await teamService.GetAllTeams(cancellationToken);
            if(result is null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost("team")]
        public async Task<IActionResult> CreateTeam([FromBody] CreateTeamCommand teamCommand , CancellationToken cancellationToken)
        {
            var result = await mediator.Send(teamCommand, cancellationToken);
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
