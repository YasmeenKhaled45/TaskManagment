using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.BuisnessLogic.Contracts.Authorization.Commands
{
    public class AddRoleCommandValidator : AbstractValidator<AddRoleCommand>
    {
        public AddRoleCommandValidator() 
        {
            RuleFor(x=>x.Name).NotEmpty().Length(3,200);
        }

    }
}
