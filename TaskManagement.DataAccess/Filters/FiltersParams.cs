using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.DataAccess.Filters
{
    public class FiltersParams
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
        public string? SortBy { get; set; } 
        public string? SortDirection { get; set; } 
        public string? Searchterm {  get; set; }
    }
}
