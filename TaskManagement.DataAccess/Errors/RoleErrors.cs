using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccess.Constants;

namespace TaskManagement.DataAccess.Errors
{
    public static class RoleErrors
    {
        public static readonly Error RoleNotFound =
       new("Role.RoleNotFound", "Role is not found");

        public static readonly Error DuplicatedRole =
            new("Role.DuplicatedRole", "Another role with the same name is already exists");
    }
}
