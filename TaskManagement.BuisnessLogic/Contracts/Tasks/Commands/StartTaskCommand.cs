using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccess.Constants;

namespace TaskManagement.BuisnessLogic.Contracts.Tasks.Commands
{
    public class StartTaskCommand : IRequest<Result>
    {
        public string UserId {  get; set; }
        public int TaskId { get; set; }
    }
}
