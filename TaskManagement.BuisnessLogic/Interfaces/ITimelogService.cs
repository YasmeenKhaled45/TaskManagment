using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccess.Constants;
using TaskManagement.DataAccess.Entities;

namespace TaskManagement.BuisnessLogic.Interfaces
{
    public interface ITimelogService
    {
        Task<Result<Timelog>> StartTimeLog(int taskId , CancellationToken cancellationToken);
        Task<Result> StopTimeLog(int taskId , CancellationToken cancellationToken);
        Task<Timelog> GetTotalTime(int taskId , CancellationToken cancellationToken);
    }
}
