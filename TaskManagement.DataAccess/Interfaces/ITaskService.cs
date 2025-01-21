using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccess.Constants;
using TaskManagement.DataAccess.Dtos.Tasks;

namespace TaskManagement.DataAccess.Interfaces
{
    public interface ITaskService
    {
        Task<TaskDto> CreateTaskAsync(CreateTask task, CancellationToken cancellationToken);
        Task <Result<TaskDto>> GetTaskById(int Id, CancellationToken cancellationToken);
        Task<Result<TaskDto>> CreateSubTask(int taskId, CreateTask task, CancellationToken cancellationToken);
        Task<Result> StartTask(int taskId, CancellationToken cancellationToken);    

    }
}
