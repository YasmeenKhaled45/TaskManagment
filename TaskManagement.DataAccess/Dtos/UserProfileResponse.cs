﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.DataAccess.Dtos
{
    public record UserProfileResponse
    (string Email , string FirstName , string LastName);
}
