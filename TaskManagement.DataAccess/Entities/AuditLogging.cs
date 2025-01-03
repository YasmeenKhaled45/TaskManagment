using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.DataAccess.Entities
{
    public class AuditLogging
    {
        public string CreateById { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedById { get; set; }
        public User? UpdatedBy { get; set; }
    }
}
