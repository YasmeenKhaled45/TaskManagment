using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccess.Constants;
using TaskManagement.DataAccess.Dtos;

namespace TaskManagement.BuisnessLogic.Contracts.Tasks.Queries
{
    public class GetTaskByIdQuery : IRequest<Result<TaskDto>>
    {
        public int Id { get; set; }

        public GetTaskByIdQuery(int id)
        {
            Id = id;
        }
    }
}
