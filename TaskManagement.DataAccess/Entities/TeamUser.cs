using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.DataAccess.Entities
{
    public class TeamUser
    {
        public int TeamUserId { get; set; }
        public int TeamId { get; set; }
        public string? UserId { get; set; }

        public Team Team { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
