using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccess.Data;
using TaskManagement.DataAccess.Dtos.Tasks;
using TaskManagement.DataAccess.Entities;
using TaskManagement.DataAccess.Interfaces;

namespace TaskManagement.BuisnessLogic.Services
{
    public class TaskService(AppDbContext context , INotficationService notficationService) : ITaskService
    {
        private readonly AppDbContext context = context;
        private readonly INotficationService notficationService = notficationService;

        public async Task<TaskDto> CreateTaskAsync(CreateTask task , CancellationToken cancellationToken)
        {
            var addedtask = task.Adapt<Tasks>();
            await context.Tasks.AddAsync(addedtask,cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await notficationService.SendNewTaskNotfications(addedtask);
            return addedtask.Adapt<TaskDto>();

        }

        public async Task<TaskDto> GetTaskById(int Id, CancellationToken cancellationToken)
        {
            var task = await context.Tasks.FindAsync(Id, cancellationToken);
            return task.Adapt<TaskDto>();
        }
    }
}
