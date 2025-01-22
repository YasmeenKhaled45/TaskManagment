using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.BuisnessLogic.Contracts.Tasks.Queries;
using TaskManagement.DataAccess.Constants;
using TaskManagement.DataAccess.Dtos.Tasks;
using TaskManagement.DataAccess.Interfaces;


namespace TaskManagement.BuisnessLogic.Contracts.Tasks.Handlers
{
    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, Result<TaskDto>>
    {
        private readonly ITaskService _taskService;

        public GetTaskByIdQueryHandler(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<Result<TaskDto>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            return await _taskService.GetTaskById(request.Id, cancellationToken);
        }
    }

}
