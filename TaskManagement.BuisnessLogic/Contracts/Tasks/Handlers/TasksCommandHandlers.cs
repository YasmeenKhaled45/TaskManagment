using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.BuisnessLogic.Contracts.Tasks.Commands;
using TaskManagement.DataAccess.Constants;
using TaskManagement.DataAccess.Dtos.Tasks;
using TaskManagement.DataAccess.Interfaces;

namespace TaskManagement.BuisnessLogic.Contracts.Tasks.Handlers
{
    public class TasksCommandHandlers(ITaskService taskService) : IRequestHandler<CreateTaskCommand,Result<TaskDto>>
    {
        private readonly ITaskService taskService = taskService;

        public async Task<Result<TaskDto>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            return await taskService.CreateTaskAsync(request, cancellationToken);
        }
    }
}
