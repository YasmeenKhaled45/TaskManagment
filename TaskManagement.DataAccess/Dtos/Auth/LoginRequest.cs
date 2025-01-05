using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.DataAccess.Dtos.Auth
{
    public record LoginRequest
    (string Email , string Password);
}
