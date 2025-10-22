using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Identity.Application.Common.Interfaces;
using ToDoList.Identity.Application.Users.RequestDto;

namespace ToDoList.Identity.Application.Users.Commands
{
    public class LoginCommand : IWithResultCommand<LoginResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
