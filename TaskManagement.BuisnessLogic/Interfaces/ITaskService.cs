using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.BuisnessLogic.Contracts.Tasks.Commands;
using TaskManagement.DataAccess.Constants;
using TaskManagement.DataAccess.Dtos;
using TaskManagement.DataAccess.Filters;

namespace TaskManagement.DataAccess.Interfaces
{
    public interface ITaskService
    {
        Task<Result<PagedList<TaskDto>>> GetAllTasks(FiltersParams filters,CancellationToken cancellationToken);
        Task<Result<TaskDto>> CreateTaskAsync( CreateTaskCommand task, CancellationToken cancellationToken);
        Task <Result<TaskDto>> GetTaskById(int Id, CancellationToken cancellationToken);
        Task<Result<TaskDto>> CreateSubTask(int taskId, CreateTaskCommand task, CancellationToken cancellationToken);
        Task<Result> StartTask(int taskId, CancellationToken cancellationToken);    
        Task<Result> EndTask(int taskId, CancellationToken cancellationToken);
    }
}
