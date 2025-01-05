using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccess.Dtos.Auth;

namespace TaskManagement.DataAccess.Validations.User
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email is required");
            RuleFor(x => x.Password).NotEmpty()
                 .WithMessage("Password must be at least 8 digits and should contain Lowercase, NonAlphanummeric and Uppercase");
        }
       
    }
}
