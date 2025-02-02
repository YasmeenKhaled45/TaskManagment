using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccess.Constants;

namespace TaskManagement.DataAccess.Errors
{
    public static class TaskErrors
    {
        public static readonly Error TaskNotFound = new("Task.Errors", "Task is not found");
        public static readonly Error TaskDate =
       new("Task.Errors", "The deadline of this task is passed.");

        public static readonly Error TaskDependency = new("Task.Errors",
            "Cannot start this subtask because the parent task is not completed.");

        public static readonly Error StartTaskError = new("Task.Errors","You have already started this task before.");
    }
}
