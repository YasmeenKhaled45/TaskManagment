using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.BuisnessLogic.Contracts.Users.Commands;
using TaskManagement.DataAccess.Constants;
using TaskManagement.DataAccess.Interfaces;

namespace TaskManagement.BuisnessLogic.Contracts.Users.Handlers
{
    public class UserCommandHandler(IUserService userService) : IRequestHandler<UpdateProfileCommand, Result>
    {
        private readonly IUserService userService = userService;

        public async Task<Result> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
           return await userService.UpdateProfileAsync(request, cancellationToken);
        }
    }
}
