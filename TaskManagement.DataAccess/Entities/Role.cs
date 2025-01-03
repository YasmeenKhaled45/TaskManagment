using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.DataAccess.Entities
{
    public class Role : IdentityRole<string>
    {

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
