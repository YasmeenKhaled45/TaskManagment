using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.DataAccess.Dtos.Comments
{
    public record CreateComment
    (int TaskId , string Content);
}
