using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccess.Entities;

namespace TaskManagement.DataAccess.Interfaces
{
    public interface INotficationService
    {
        Task SendNewTaskNotfications(Tasks task);
        Task HandleTaskDeadline();
        Task SendTaskReminderDate();
    }
}
