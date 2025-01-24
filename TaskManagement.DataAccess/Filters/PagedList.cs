 using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.DataAccess.Filters
{
    public class PagedList<T>(List<T> items, int pageNumber, int count, int pageSize)
    {
        public List<T> Items { get; set; } = items;
        public int PageNumber { get; set; } = pageNumber;
        public int TotalPages { get; set; } = (int)Math.Ceiling(count / (double)pageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> data ,int pageNumber, int pageSize,CancellationToken cancellationToken = default)
        {
            var count = await data.CountAsync(cancellationToken);
            var items = await data.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items,pageNumber,count,pageSize);
        }
    }
}
