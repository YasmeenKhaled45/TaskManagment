﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.DataAccess.Responses.User
{
    public record AuthResponse
    (
          string Id,
        string FirstName,
        string LastName,
        string? Email,
        string Token,
        int ExpiresIn,
        string RefreshToken,
        DateTime RefreshTokenExpiration
     );
}
