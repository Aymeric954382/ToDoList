using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.TaskStateService.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
        protected IMediator Mediator { get; }
        protected IMapper Mapper { get; }

        protected BaseController(IMediator mediator, IMapper mapper)
        {
            Mediator = mediator;
            Mapper = mapper;
        }

        protected Guid UserId
        {
            get
            {
                if (User?.Identity?.IsAuthenticated != true)
                    throw new UnauthorizedAccessException();

                var value = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (value is null)
                    throw new UnauthorizedAccessException("UserId claim missing");

                if (!Guid.TryParse(value, out var userId))
                    throw new UnauthorizedAccessException("Invalid UserId");

                return userId;
            }
        }
    }
}
