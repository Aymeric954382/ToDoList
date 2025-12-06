using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ToDoList.Gateway.WebAPI.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMediator Mediator;

        protected BaseController(IMediator mediator)
        {
            Mediator = mediator;
        }

        protected Guid UserId
        {
            get
            {
                var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return Guid.TryParse(id, out var guid) ? guid : Guid.Empty;
            }
        }
    }
}