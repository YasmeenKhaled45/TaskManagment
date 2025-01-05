using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.DataAccess.Dtos.Comments;
using TaskManagement.DataAccess.Interfaces.Comments;

namespace TaskManagment.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController(ICommentRepository repository) : ControllerBase
    {
        private readonly ICommentRepository repository = repository;

        [HttpPost("{id}")]
        [Authorize]
        public async Task<IActionResult> AddComment([FromRoute]int id , CreateComment comment , CancellationToken cancellationToken)
        {
            var result = await repository.CreateComment(id, comment , cancellationToken);
            return Ok(result);
        }
    }
}
