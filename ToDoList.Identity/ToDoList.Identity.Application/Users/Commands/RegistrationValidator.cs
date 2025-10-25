using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ToDoList.Identity.Application.Users.Commands
{
    public class RegistrationValidator : AbstractValidator<RegistrationCommand>
    {
        public RegistrationValidator()
        {
            RuleFor(command =>
                command.Password).MinimumLength(8).MaximumLength(64).NotEmpty().NotNull();
            RuleFor(command =>
                command.Email).EmailAddress().NotEmpty().NotNull();
            RuleFor(command =>
                command.DateOfBirth).Must(date => date < DateTime.Now).NotEmpty().NotNull();
            RuleFor(command =>
                command.UserName).MaximumLength(30).NotNull().NotEmpty();
        }
    }
}
