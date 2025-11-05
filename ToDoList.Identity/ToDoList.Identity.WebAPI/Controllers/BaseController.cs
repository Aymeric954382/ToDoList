using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Identity.WebAPI.Controllers
{
    [ApiController]
    [Route("identity/[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => 
            _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
