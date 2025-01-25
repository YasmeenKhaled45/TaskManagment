using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccess.Constants;

namespace TaskManagement.DataAccess.Errors
{
    public static class AuthErrors
    {
        public static readonly Error InvalidCredentials = 
            new("User.InvalidCredentials", "Invalid email or password");
        public static readonly Error LockedUser =
       new("User.LockedUser", "Locked user, please contact your administrator");
        public static readonly Error DuplicatedEmail =
      new("User.DuplicatedEmail", "Another user with the same email is already exists");
        public static readonly Error UserName = new ("Registration Failed!", "UserName is not being set correctly.");
    }
}
