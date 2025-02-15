﻿using System;
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
        public int? ParentTaskId { get; set; }
        public virtual Tasks ParentTask { get; set; }
        public virtual ICollection<Tasks> SubTasks { get; set; } = new List<Tasks>();
        public string? AssignedToUserId { get; set; }
        public User User { get; set; }
        public int? AssignedToTeamId { get; set; }
        public Team Team { get; set; }
        public ICollection<Comments> Comments { get; set; } = new List<Comments>();
        public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
        public ICollection<Timelog> Timelogs { get; set; } = new List<Timelog>(); 
    }
}
