using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.DataAccess.Constants
{
    public record Error(string Code, string Message)
    {
        public static readonly Error none = new Error(string.Empty, string.Empty);
    }
}
