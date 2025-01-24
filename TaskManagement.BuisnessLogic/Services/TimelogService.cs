using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.BuisnessLogic.Interfaces;
using TaskManagement.DataAccess.Constants;
using TaskManagement.DataAccess.Data;
using TaskManagement.DataAccess.Entities;
using TaskManagement.DataAccess.Enums;

namespace TaskManagement.BuisnessLogic.Services
{
    public class TimelogService(AppDbContext context) : ITimelogService
    {
        private readonly AppDbContext context = context;

        public Task<Timelog> GetTotalTime(int taskId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Timelog>> StartTimeLog(int taskId, CancellationToken cancellationToken)
        {
            var task = await context.Tasks
                                  .Include(t => t.Timelogs)
                                  .FirstOrDefaultAsync(t => t.Id == taskId, cancellationToken);
            var existingTime = task!.Timelogs.LastOrDefault(t => t.EndTime == null);
            if (existingTime != null)
                return Result.Failure<Timelog>(new Error("Start task failed!", "There is already an active time log for this task."));
            var newtimelog = new Timelog
            {
                TaskId = task.Id,
                StartTime = DateTime.Now,
            };
            task.Timelogs.Add(newtimelog);
            await context.SaveChangesAsync(cancellationToken);
            return Result.Success(newtimelog);
        }

        public async Task<Result> StopTimeLog(int taskId, CancellationToken cancellationToken)
        {
            var task = await context.Tasks.Include(t => t.Timelogs)
                .FirstOrDefaultAsync(t=>t.Id == taskId,cancellationToken);
            if (task == null)
                return Result.Failure(new Error("End task Failed!", "Task not found."));
            var activeTimeLog = task.Timelogs.LastOrDefault(tl => tl.EndTime == null);
            if (activeTimeLog == null)
               return Result.Failure(new Error("End task failed!","No active time log found for this task."));
            activeTimeLog.EndTime = DateTime.Now;
            task.Status = Status.Completed;
            await context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
