using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccess.Constants;
using TaskManagement.DataAccess.Dtos;

namespace TaskManagement.BuisnessLogic.Contracts.Users.Queries
{
    public class GetUserProfileQuery : IRequest<Result<UserProfileResponse>>
    {
       public string UserId {  get; set; }
        public GetUserProfileQuery(string userId)
        {
            UserId = userId;
        }
    }
}
