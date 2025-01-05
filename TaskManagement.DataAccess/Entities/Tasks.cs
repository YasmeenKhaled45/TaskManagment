using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TaskManagement.DataAccess.Enums;

namespace TaskManagement.DataAccess.Entities
{
    public class Tasks : AuditLogging
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateOnly DueDate { get; set; }
        public TaskPriority TaskPriority { get; set; } = TaskPriority.Low;
        public Status Status { get; set; } = Status.ToDo;

       
        public ICollection<Comments> Comments { get; set; } = new List<Comments>();
        public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
    }
}
