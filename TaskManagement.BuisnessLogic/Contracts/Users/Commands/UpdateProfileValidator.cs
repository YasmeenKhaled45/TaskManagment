using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.BuisnessLogic.Contracts.Users.Commands
{
    public class UpdateProfileValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileValidator() 
        {
            RuleFor(x=>x.FirstName).NotEmpty()
                .Length(3,100);
            RuleFor(x=>x.LastName).NotEmpty().Length(3,100);
        }
    }
}
