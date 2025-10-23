using Microsoft.AspNetCore.Mvc;
using ToDoList.Identity.Application.Users.Commands;
using ToDoList.Identity.Application.Users.Queries;
using ToDoList.Identity.Application.Users.RequestDto;

namespace ToDoList.Identity.WebAPI.Controllers
{
    public class AuthController : BaseController
    {
        [HttpPost("login")]
        public async Task<ActionResult<LoginResult>> Login([FromBody] LoginQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
        [HttpPost("registration")]
        public async Task<ActionResult<RegistrationResult>> Registration([FromBody] RegistrationCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

    }
}
