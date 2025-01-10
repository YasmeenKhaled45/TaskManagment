using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccess.Entities;

namespace TaskManagement.DataAccess.Dtos.Teams
{
    public record CreateTeamDto
    (string Name , List<string> UserIds );
}
