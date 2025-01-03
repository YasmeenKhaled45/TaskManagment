using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.DataAccess.Entities
{
    public class Team
    {
        public int TeamId { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<TeamUser> TeamUsers { get; set; } = new List<TeamUser>();
        public ICollection<Tasks> Tasks { get; set; } = new List<Tasks>();
    }
}
