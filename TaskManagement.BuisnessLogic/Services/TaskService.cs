using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.BuisnessLogic.Contracts.Tasks.Commands;
using TaskManagement.DataAccess.Constants;
using TaskManagement.DataAccess.Data;
using TaskManagement.DataAccess.Dtos;
using TaskManagement.DataAccess.Entities;
using System.Linq.Dynamic.Core;
using TaskManagement.DataAccess.Enums;
using TaskManagement.DataAccess.Filters;
using TaskManagement.DataAccess.Interfaces;
using TaskManagement.DataAccess.Validations.Tasks;
using MailKit.Search;
using TaskManagement.BuisnessLogic.Interfaces;

namespace TaskManagement.BuisnessLogic.Services
{
    public class TaskService(AppDbContext context , INotficationService notficationService,ITimelogService timelogService) : ITaskService
    {
        private readonly AppDbContext context = context;
        private readonly INotficationService notficationService = notficationService;
        private readonly ITimelogService timelogService1 = timelogService;

        public async Task<Result<PagedList<TaskDto>>> GetAllTasks(FiltersParams filters, CancellationToken cancellationToken)
        {
            var query = context.Tasks.AsQueryable();

            if (!string.IsNullOrEmpty(filters.SortBy))
            {
                query = query.OrderBy($"{filters.SortBy} {filters.SortDirection}");
            }
            if (!string.IsNullOrEmpty(filters.Searchterm))
            {
                query = query.Where(x => x.Title.Contains(filters.Searchterm));
            }
            var source = query.ProjectToType<TaskDto>().AsNoTracking();
            var questions = await PagedList<TaskDto>.CreateAsync(source, filters.PageNumber, filters.PageSize, cancellationToken);
            return Result.Success(questions);
        }
        public async Task<Result<TaskDto>> CreateSubTask(int taskId, CreateTaskCommand task, CancellationToken cancellationToken)
        {
            var res = await context.Tasks.FirstOrDefaultAsync(t => t.Id == taskId, cancellationToken);
            if (res == null)
                return Result.Failure<TaskDto>(new Error("Creation failed!", "Task not found with this Id"));
            var subtask = task.Adapt<Tasks>();
            subtask.ParentTaskId = taskId;
            subtask.Status = Status.ToDo;

            return await CreateTaskBaseAsync(subtask, cancellationToken);
        }
        private async Task<Result<TaskDto>> CreateTaskBaseAsync(Tasks task, CancellationToken cancellationToken)
        {
            context.Tasks.Add(task);
            await context.SaveChangesAsync(cancellationToken);
            await notficationService.SendNewTaskNotfications(task);

            return Result.Success(task.Adapt<TaskDto>());
        }
        public async Task<Result<TaskDto>> CreateTaskAsync(CreateTaskCommand task , CancellationToken cancellationToken)
        {
            var addedTask = task.Adapt<Tasks>();
            addedTask.Status = Status.ToDo;
            return await CreateTaskBaseAsync(addedTask, cancellationToken);
        }

        public async Task<Result<TaskDto>> GetTaskById(int Id, CancellationToken cancellationToken)
        {
            var task = await context.Tasks.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);

            if (task == null)
                return Result.Failure<TaskDto>(new Error("TaskNotFound", $"Task with Id {Id} not found"));

            return Result.Success(task.Adapt<TaskDto>());
        }

        public async Task<Result> StartTask(int taskId, CancellationToken cancellationToken)
        {
            var task = await context.Tasks.FirstOrDefaultAsync(t => t.Id == taskId, cancellationToken);
            if (task == null)
                return Result.Failure(new Error("Start task failed!", "Task not found with this Id"));
            if(task.Status == Status.Late)
                return Result.Failure(new Error("Start task failed!", "The deadline of this task is passed."));
            if (task.ParentTask != null && task.ParentTask.Status != Status.Completed)
            {
                var parentTaskName = task.ParentTask.Title; 
                return Result.Failure(new Error("Start task failed!",
                    $"Cannot start this subtask because the parent task '{parentTaskName}' is not completed."));
            }
            var timeLog = await timelogService1.StartTimeLog(taskId, cancellationToken);
            task.Status = Status.InProgress;
            await context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> EndTask(int taskId, CancellationToken cancellationToken)
        {
            var task = await context.Tasks.FirstOrDefaultAsync(t => t.Id == taskId, cancellationToken);
            if (task == null)
                return Result.Failure(new Error("End task failed!", "Task not found."));

            if (task.Status != Status.InProgress)
                return Result.Failure(new Error("End task failed!", "The task is not in progress."));

            var timeLogResult = await timelogService1.StopTimeLog(taskId, cancellationToken);

            if (!timeLogResult.IsSuccess)
                return timeLogResult; 
            task.Status = Status.Completed;
            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
