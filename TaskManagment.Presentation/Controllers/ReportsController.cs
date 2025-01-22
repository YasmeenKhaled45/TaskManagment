using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagement.DataAccess.Data;
using TaskManagement.DataAccess.Enums;

namespace TaskManagment.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReportsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("task-status")]
        public async Task<IActionResult> GetTaskStatusCounts()
        {
            var taskStats = await _context.Tasks
                .GroupBy(t => t.Status)
                .Select(g => new
                {
                    Status = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            return Ok(taskStats);
        }

        [HttpGet("overdue-tasks")]
        public async Task<IActionResult> GetOverdueTasksCount()
        {
            var overdueTasksCount = await _context.Tasks
                .Where(t => t.DueDate < DateOnly.FromDateTime(DateTime.UtcNow) && t.Status != Status.Completed)
                .CountAsync();

            return Ok(new { OverdueTasks = overdueTasksCount });
        }

        // Endpoint for Task Completion Trends Over Time (e.g., Completed vs In Progress)
        [HttpGet("task-trends")]
        public async Task<IActionResult> GetTaskTrends()
        {
            var taskTrends = await _context.Tasks
             .GroupBy(t => t.CreatedOn.Month) 
               .Select(g => new
              {
                 Month = g.Key,
                  Completed = g.Count(t => t.Status == Status.Completed),
                  InProgress = g.Count(t => t.Status == Status.InProgress)
              }).OrderBy(g => g.Month).ToListAsync();

            return Ok(taskTrends);
        }
    }
}
