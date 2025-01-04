using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccess.Enums;

namespace TaskManagement.DataAccess.Dtos.Tasks
{
    public record CreateTask
    (string Title, string Description, DateTime DueDate, TaskPriority TaskPriority, Status Status);
}
