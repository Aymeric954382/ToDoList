using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Identity.Application.Users.RequestDto
{
    public class RegistrationResult 
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
    }
}
