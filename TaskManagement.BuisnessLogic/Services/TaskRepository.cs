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
    public class TaskRepository(AppDbContext context) : ITaskRepository
    {
        private readonly AppDbContext context = context;

        public async Task<TaskDto> CreateTaskAsync(CreateTask task , CancellationToken cancellationToken)
        {
            var addedtask = task.Adapt<Tasks>();
            await context.Tasks.AddAsync(addedtask,cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return addedtask.Adapt<TaskDto>();

        }

        public async Task<TaskDto> GetTaskById(int Id, CancellationToken cancellationToken)
        {
            var task = await context.Tasks.FindAsync(Id, cancellationToken);
            return task.Adapt<TaskDto>();
        }
    }
}
