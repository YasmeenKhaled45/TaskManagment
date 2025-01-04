using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.DataAccess.Dtos.Auth
{
    public record RegisterRequestdto( string Email , string Password , string FirstName , string LastName);
}
