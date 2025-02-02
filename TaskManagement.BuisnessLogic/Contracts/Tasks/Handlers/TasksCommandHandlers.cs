using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.BuisnessLogic.Contracts.Tasks.Commands;
using TaskManagement.BuisnessLogic.Services;
using TaskManagement.DataAccess.Constants;
using TaskManagement.DataAccess.Data;
using TaskManagement.DataAccess.Dtos;
using TaskManagement.DataAccess.Enums;
using TaskManagement.DataAccess.Errors;
using TaskManagement.DataAccess.Interfaces;

namespace TaskManagement.BuisnessLogic.Contracts.Tasks.Handlers
{
    public class TasksCommandHandlers(ITaskService taskService,AppDbContext context) : IRequestHandler<CreateTaskCommand,Result<TaskDto>>,
        IRequestHandler<StartTaskCommand,Result>
    {
        private readonly ITaskService taskService = taskService;
        private readonly AppDbContext context = context;

        public async Task<Result<TaskDto>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var result =  await taskService.CreateTaskAsync(request, cancellationToken);
            return result;
        }

        public async Task<Result> Handle(StartTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await context.Tasks.FirstOrDefaultAsync(t => t.Id == request.TaskId, cancellationToken);
            if (task == null)
                return Result.Failure(TaskErrors.TaskNotFound);

            if (task.Status == Status.Late)
                return Result.Failure(TaskErrors.TaskDate);

            if (task.ParentTask != null && task.ParentTask.Status != Status.Completed)
                return Result.Failure(TaskErrors.TaskDependency);

            var hasStartedBefore = await context.Timelogs.AnyAsync(x => x.TaskId == request.TaskId &&
                     x.UserId == request.UserId, cancellationToken);
            if (hasStartedBefore)
                return Result.Failure(TaskErrors.StartTaskError);

            return await taskService.StartTask(request, cancellationToken);
    }
    }
}
