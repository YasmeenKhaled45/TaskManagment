using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.BuisnessLogic.Contracts.Authorization.Commands;
using TaskManagement.BuisnessLogic.Contracts.Authorization.Queries;

namespace TaskManagment.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;

        [HttpGet("all-roles")]
        public async Task<IActionResult> AllRoles()
        {
            var result = await mediator.Send(new GetRolesListQuery());
            return Ok(result);
        }
        [HttpPost("")]
        public async Task<IActionResult> RoleAsync(AddRoleCommand roleCommand,CancellationToken cancellationToken)
        {
            var result = await mediator.Send(roleCommand,cancellationToken);
            return Ok(result);
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateRole([FromRoute]string Id ,UpdateRoleCommand roleCommand,CancellationToken cancellationToken)
        {
            roleCommand.RoleId = Id;
            var result = await mediator.Send(roleCommand, cancellationToken);
            return Ok(result);
        }
    }
}
