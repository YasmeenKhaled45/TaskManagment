using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccess.Constants;
using TaskManagement.DataAccess.Dtos;
using TaskManagement.DataAccess.Enums;

namespace TaskManagement.BuisnessLogic.Contracts.Tasks.Commands
{
    public class CreateTaskCommand : IRequest<Result<TaskDto>>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly DueDate { get; set; }
        public TaskPriority TaskPriority { get; set; }

     
    }
}