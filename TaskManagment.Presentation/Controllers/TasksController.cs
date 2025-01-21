using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.BuisnessLogic.DataSantization;
using TaskManagement.DataAccess.Dtos.Tasks;
using TaskManagement.DataAccess.Interfaces;

namespace TaskManagment.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController(ITaskService repository) : ControllerBase
    {
        private readonly ITaskService repository = repository;

        [HttpPost]
        [Authorize]
        [ServiceFilter(typeof(InputSanitizationFilter))]
        public async Task<IActionResult> Task([FromForm]CreateTask task , CancellationToken cancellationToken)
        {
            var result = await repository.CreateTaskAsync(task, cancellationToken);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask([FromRoute] int id , CancellationToken cancellationToken)
        {
            var result = await repository.GetTaskById(id, cancellationToken);
            if(result == null)
                return NotFound("Task not found ");
            return Ok(result);
        }
        [HttpPost("{taskId}/subtasks")]
        [Authorize]
        public async Task<IActionResult> SubTask(int taskId , CreateTask task , CancellationToken cancellationToken)
        {
            var result = await repository.CreateSubTask(taskId, task, cancellationToken);
            return Ok(result);
        }

        [HttpPost("{taskId}/startTask")]
        public async Task<IActionResult> StartTask(int taskId, CancellationToken cancellationToken)
        {
            var result = await repository.StartTask(taskId, cancellationToken);
            return Ok(result);
        }
    }
}
