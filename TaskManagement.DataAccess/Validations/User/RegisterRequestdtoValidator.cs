using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccess.Dtos.Auth;

namespace TaskManagement.DataAccess.Validations.User
{
    public class RegisterRequestdtoValidator : AbstractValidator<RegisterRequestdto>
    {
        public RegisterRequestdtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x=>x.Password).NotEmpty()
                .WithMessage("Password must be at least 8 digits and should contain Lowercase, NonAlphanummeric and Uppercase");
            RuleFor(x => x.FirstName).NotEmpty().Length(3, 50);
            RuleFor(x => x.LastName).NotEmpty().Length(3, 50);
        }
    }
}
