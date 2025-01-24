using MediatR;
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
    public class TasksQueryHandler : IRequestHandler<GetTaskByIdQuery, Result<TaskDto>>,
         IRequestHandler<GetTasksQueryList,Result<PagedList<TaskDto>>>
    {
        private readonly ITaskService _taskService;

        public TasksQueryHandler(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<Result<TaskDto>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            return await _taskService.GetTaskById(request.Id, cancellationToken);
        }

        public async Task<Result<PagedList<TaskDto>>> Handle(GetTasksQueryList request, CancellationToken cancellationToken)
        {
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
