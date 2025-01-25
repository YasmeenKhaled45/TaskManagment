using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccess.Constants;

namespace TaskManagement.BuisnessLogic.Contracts.Users.Commands
{
    public class UpdateProfileCommand : IRequest<Result>
    {
        public string UserId { get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
    }
}
