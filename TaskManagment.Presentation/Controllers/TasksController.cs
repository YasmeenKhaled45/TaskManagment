using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.BuisnessLogic.Contracts.Tasks.Commands;
using TaskManagement.BuisnessLogic.Contracts.Tasks.Queries;
using TaskManagement.BuisnessLogic.DataSantization;
using TaskManagement.DataAccess.Filters;
using TaskManagement.DataAccess.Interfaces;

namespace TaskManagment.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController(ITaskService repository,IMediator mediator) : ControllerBase
    {
        private readonly ITaskService repository = repository;
        private readonly IMediator mediator = mediator;

        [HttpGet("all-tasks")]
        public async Task<IActionResult> AllTasks([FromQuery] FiltersParams filters , CancellationToken cancellationToken) 
        {
            var query = new GetTasksQueryList
            {
                PageNumber = filters.PageNumber,
                PageSize = filters.PageSize,
                SortBy = filters.SortBy,
                SortDirection = filters.SortDirection,
                Searchterm = filters.Searchterm,
            };

            var response = await mediator.Send(query, cancellationToken);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask([FromRoute] int id, CancellationToken cancellationToken)
        {
            var query = new GetTaskByIdQuery(id);
            var result = await mediator.Send(query, cancellationToken);

            if (result.IsFailure)
                return NotFound(result.Error.Message);

            return Ok(result.Value); 
        }
        [HttpPost]
        [Authorize]
        [ServiceFilter(typeof(InputSanitizationFilter))]
        public async Task<IActionResult> Tasks([FromForm] CreateTaskCommand task , CancellationToken cancellationToken)
        {
            var result = await mediator.Send(task, cancellationToken);
            return Ok(result);
        }
     
        [HttpPost("{taskId}/subtasks")]
        [Authorize]
        public async Task<IActionResult> SubTask(int taskId , CreateTaskCommand task , CancellationToken cancellationToken)
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
        [HttpPost("{taskId}/endTask")]
        public async Task<IActionResult> EndTask(int taskId , CancellationToken cancellationToken)
        {
            var result = await repository.EndTask(taskId, cancellationToken);
            return Ok(result);
        }
    }
}
