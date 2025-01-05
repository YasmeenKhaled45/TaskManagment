using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccess.Constants;
using TaskManagement.DataAccess.Dtos.Auth;
using TaskManagement.DataAccess.Entities;
using TaskManagement.DataAccess.Interfaces.User;

namespace TaskManagement.BuisnessLogic.Services
{
    public class UserRepository(UserManager<User> userManager) : IUserRepository
    {
        private readonly UserManager<User> _userManager = userManager;

        public async Task<Result> RegisterAsync(RegisterRequestdto request, CancellationToken cancellationToken)
        {
            var emailexists =  await _userManager.FindByEmailAsync(request.Email);
            if(emailexists is not null) 
                return Result.Failure(new Error("Registration Failed!", "Email already exists!"));

            var user = request.Adapt<User>();
            user.UserName = request.Email;
            if (string.IsNullOrEmpty(user.UserName))
            {
                return Result.Failure(new Error("Registration Failed!", "UserName is not being set correctly."));
            }

            var res = await _userManager.CreateAsync(user,request.Password);
            if (!res.Succeeded)
            {
                var errors = string.Join(", ", res.Errors.Select(e => e.Description));
                return Result.Failure(new Error("Registration Failed!", errors));
            }

            var roleResult = await _userManager.AddToRoleAsync(user, "User");
            if (!roleResult.Succeeded)
            {
                var roleErrors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                return Result.Failure(new Error("Role Assignment Failed!", roleErrors));
            }

            return Result.Success();
        }
    }
}
