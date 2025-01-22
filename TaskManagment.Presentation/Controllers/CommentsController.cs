using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.BuisnessLogic.Contracts.Comments.Commands;
using TaskManagement.BuisnessLogic.DataSantization;
using TaskManagement.DataAccess.Dtos.Comments;
using TaskManagement.DataAccess.Interfaces;

namespace TaskManagment.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;

        [HttpPost("{taskId}")]
        [ServiceFilter(typeof(InputSanitizationFilter))]
        [Authorize]
        public async Task<IActionResult> AddComment(int taskId ,CreateCommentCommand comment , CancellationToken cancellationToken)
        {
            var result = await mediator.Send(comment, cancellationToken);
            return Ok(result);
        }
    }
}
