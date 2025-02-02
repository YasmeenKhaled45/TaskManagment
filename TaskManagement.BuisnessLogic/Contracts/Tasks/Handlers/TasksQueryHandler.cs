using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.BuisnessLogic.Contracts.Tasks.Queries;
using TaskManagement.DataAccess.Constants;
using TaskManagement.DataAccess.Dtos;
using TaskManagement.DataAccess.Filters;
using TaskManagement.DataAccess.Interfaces;


namespace TaskManagement.BuisnessLogic.Contracts.Tasks.Handlers
{
    public class TasksQueryHandler(ITaskService taskService , ILogger<TasksQueryHandler> logger) : IRequestHandler<GetTaskByIdQuery, Result<TaskDto>>,
         IRequestHandler<GetTasksQueryList,Result<PagedList<TaskDto>>>
    {
        private readonly ITaskService _taskService = taskService;
        private readonly ILogger<TasksQueryHandler> logger = logger;

        public async Task<Result<TaskDto>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _taskService.GetTaskById(request.Id, cancellationToken);
            if (!result.IsSuccess)
            {
                logger.LogWarning("Task {TaskId} not found.", request.Id);
                return result;
            }
            return result;
        }

        public async Task<Result<PagedList<TaskDto>>> Handle(GetTasksQueryList request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Processing GetTasksQueryList with filters: {Filters}", request);
            if (request.PageNumber <= 0) request.PageNumber = 1;
            if (request.PageSize <= 0) request.PageSize = 10;
            if (string.IsNullOrWhiteSpace(request.SortBy)) request.SortBy = "CreatedAt"; 
            if (string.IsNullOrWhiteSpace(request.SortDirection)) request.SortDirection = "desc";
            var filters = new FiltersParams
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SortBy = request.SortBy,
                SortDirection = request.SortDirection,
                Searchterm = request.Searchterm,
            };
            return await _taskService.GetAllTasks(filters, cancellationToken);
        }
    }

}
