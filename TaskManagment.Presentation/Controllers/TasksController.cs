﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.DataAccess.Dtos.Tasks;
using TaskManagement.DataAccess.Interfaces.Tasks;

namespace TaskManagment.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository repository;

        public TasksController(ITaskRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateTask([FromForm]CreateTask task , CancellationToken cancellationToken)
        {
            var res = await repository.CreateTaskAsync(task, cancellationToken);
            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask([FromRoute] int id , CancellationToken cancellationToken)
        {
            var result = await repository.GetTaskById(id, cancellationToken);
            if(result == null)
                return NotFound("Task not found ");
            return Ok(result);
        }
    }
}
