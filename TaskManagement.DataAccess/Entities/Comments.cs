using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.DataAccess.Entities
{
    public class Comments : AuditLogging
    {
        public int Id { get; set; }
        public int TaskId { get; set; }

        public string Content { get; set; } = string.Empty;

        public Tasks Task { get; set; } = null!;

    }
}
