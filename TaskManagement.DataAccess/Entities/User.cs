using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TaskManagement.DataAccess.Entities
{
    public sealed class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public ICollection<Tasks> AssignedTasks { get; set; } = new List<Tasks>();
        public ICollection<TeamUser> TeamUsers { get; set; } = new List<TeamUser>();
        public ICollection<Timelog> Timelogs { get; set; } = new List<Timelog>();
        public List<RefreshToken> RefreshTokens { get; set; } = [];
    }
}
