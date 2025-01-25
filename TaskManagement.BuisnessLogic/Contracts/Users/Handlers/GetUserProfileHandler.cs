using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.BuisnessLogic.Contracts.Users.Queries;
using TaskManagement.DataAccess.Constants;
using TaskManagement.DataAccess.Dtos;
using TaskManagement.DataAccess.Interfaces;

namespace TaskManagement.BuisnessLogic.Contracts.Users.Handlers
{
    public class GetUserProfileHandler(IUserService userService) : IRequestHandler<GetUserProfileQuery, Result<UserProfileResponse>>
    {
        private readonly IUserService userService = userService;

        public async Task<Result<UserProfileResponse>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            return await userService.GetUserProfile(request.UserId);
        }
    }
}
