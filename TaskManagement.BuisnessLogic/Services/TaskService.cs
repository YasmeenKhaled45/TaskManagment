using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccess.Constants;
using TaskManagement.DataAccess.Data;
using TaskManagement.DataAccess.Dtos.Tasks;
using TaskManagement.DataAccess.Entities;
using TaskManagement.DataAccess.Enums;
using TaskManagement.DataAccess.Interfaces;

namespace TaskManagement.BuisnessLogic.Services
{
    public class TaskService(AppDbContext context , INotficationService notficationService) : ITaskService
    {
        private readonly AppDbContext context = context;
        private readonly INotficationService notficationService = notficationService;

        public async Task<Result<TaskDto>> CreateSubTask(int taskId, CreateTask task, CancellationToken cancellationToken)
        {
            var result = await context.Tasks.Include(x => x.SubTasks).FirstOrDefaultAsync(x => x.Id == taskId, cancellationToken);
            if (result == null)
                return Result.Failure<TaskDto>(new Error("Creation failed!", "Task not found with this Id"));
            var subtask = task.Adapt<Tasks>();
            subtask.ParentTaskId = taskId;
            subtask.Status = Status.ToDo;
            await context.Tasks.AddAsync(subtask, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await notficationService.SendNewTaskNotfications(subtask);
            return Result.Success(subtask.Adapt<TaskDto>());
        }

        public async Task<TaskDto> CreateTaskAsync(CreateTask task , CancellationToken cancellationToken)
        {
            var addedtask = task.Adapt<Tasks>();
            addedtask.Status = Status.ToDo;
            await context.Tasks.AddAsync(addedtask,cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await notficationService.SendNewTaskNotfications(addedtask);
            return addedtask.Adapt<TaskDto>();

        }

        public async Task<Result<TaskDto>> GetTaskById(int Id, CancellationToken cancellationToken)
        {
            var task = await context.Tasks.FindAsync(Id, cancellationToken);
            if(task == null)
                return Result.Failure<TaskDto>(new Error("Creation failed!", "Task not found with this Id"));
            return Result.Success(task.Adapt<TaskDto>());
        }

        public async Task<Result> StartTask(int taskId, CancellationToken cancellationToken)
        {
            var task = await context.Tasks.Include(x => x.ParentTask).FirstOrDefaultAsync(x => x.Id == taskId, cancellationToken);
            if (task == null)
                return Result.Failure(new Error("Start task failed!", "Task not found with this Id"));
            if(task.Status == Status.Late)
            {
                return Result.Failure(new Error("Start task failed!", "The deadline of this task is passed."));
            }
            if (task.ParentTask != null && task.ParentTask.Status != Status.Completed)
            {
                var parentTaskName = task.ParentTask.Title; 
                return Result.Failure(new Error("Start task failed!",
                    $"Cannot start this subtask because the parent task '{parentTaskName}' is not completed."));
            }

            task.Status = Status.InProgress;
            await context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
