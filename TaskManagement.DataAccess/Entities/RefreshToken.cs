
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.DataAccess.Entities
{
    [Owned]
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
        public DateTime CreatedOn => DateTime.UtcNow;
        public bool IsExpired => DateTime.UtcNow >= ExpiresOn;

    }

}
