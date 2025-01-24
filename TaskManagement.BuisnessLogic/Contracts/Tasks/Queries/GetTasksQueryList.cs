using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccess.Constants;
using TaskManagement.DataAccess.Dtos;
using TaskManagement.DataAccess.Filters;

namespace TaskManagement.BuisnessLogic.Contracts.Tasks.Queries
{
    public class GetTasksQueryList : IRequest<Result<PagedList<TaskDto>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public string? SortDirection { get; set; }
        public string? Searchterm {  get; set; }   
      
    }
}
