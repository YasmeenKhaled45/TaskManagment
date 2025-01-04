using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccess.Constants;
using TaskManagement.DataAccess.Dtos.Tasks;

namespace TaskManagement.DataAccess.Interfaces.Tasks
{
    public interface ITaskRepository
    {
        Task<TaskDto> CreateTaskAsync(CreateTask task , CancellationToken cancellationToken);
    }
}
