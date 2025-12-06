using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Gateway.WebAPI.Controller
{
    [ApiVersion("1.0")]
    public class TaskController : BaseController
    {
        public TaskController(IMediator mediator) : base(mediator) { }
    }
}
