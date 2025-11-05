using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Identity.Application.Users.Queries
{
    public class LoginValidator : AbstractValidator<LoginQuery>
    {
        public LoginValidator()
        {
            RuleFor(command =>
                command.Password).NotEmpty().NotNull();
            RuleFor(command =>
                command.Email).EmailAddress().NotEmpty().NotNull();
        }
    }
}
