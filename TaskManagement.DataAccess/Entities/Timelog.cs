using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.DataAccess.Entities
{
    public class Timelog
    {
        public int Id {  get; set; }
        public int TaskId { get; set; }
        public Tasks Task { get; set; }
        public string? UserId {  get; set; }
        public User User { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public TimeSpan Duration => (EndTime ?? DateTime.Now) - StartTime;
    }
}
