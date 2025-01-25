using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagement.BuisnessLogic.Contracts.Users.Commands;
using TaskManagement.BuisnessLogic.Contracts.Users.Queries;
using TaskManagement.DataAccess.Interfaces;

namespace TaskManagment.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IMediator mediator) : ControllerBase
    {
        [Authorize]
        [HttpGet("user-profile")]
        public async Task<IActionResult> UserProfile()
        {
            var userId = User.FindFirstValue("uid");
            var result = await mediator.Send(new GetUserProfileQuery(userId!));
            return Ok(result);
        }
        [Authorize]
        [HttpPut("profile-info")]
        public async Task<IActionResult> UserProfile([FromBody]UpdateProfileCommand command , CancellationToken cancellationToken)
        {
            var userId = User.FindFirstValue("uid");
            command.UserId = userId!;
            var result = await mediator.Send(command,cancellationToken);
            return Ok(result);
        }
    }
}
