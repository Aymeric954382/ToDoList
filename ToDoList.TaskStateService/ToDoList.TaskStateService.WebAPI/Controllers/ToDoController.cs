using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.TaskStateService.Application.Features.ToDoItems.Commands.ChangeToDoPriority;
using ToDoList.TaskStateService.Application.Features.ToDoItems.Commands.ChangeToDoStatus;
using ToDoList.TaskStateService.Application.Features.ToDoItems.Commands.CreateToDo;
using ToDoList.TaskStateService.Application.Features.ToDoItems.Commands.DeleteToDo;
using ToDoList.TaskStateService.Application.Features.ToDoItems.Queries.GetByOverdueToDos;
using ToDoList.TaskStateService.Application.Features.ToDoItems.Queries.GetByPriority;
using ToDoList.TaskStateService.Application.Features.ToDoItems.Queries.GetByStatus;
using ToDoList.TaskStateService.Application.Features.ToDoItems.Queries.GetListToDo;
using ToDoList.TaskStateService.Domain.ValueObjects;
using ToDoList.TaskStateService.WebAPI.Models.RequestDto.Change;
using ToDoList.TaskStateService.WebAPI.Models.RequestDto.Create;
using ToDoList.TaskStateService.WebAPI.Models.RequestDto.Delete;

namespace ToDoList.TaskStateService.WebAPI.Controllers
{
    /// <summary>
    /// Provides endpoints for managing the state of to-do items.
    /// </summary>
    /// <remarks>
    /// This controller delegates all business logic to the application layer via MediatR.
    /// Response formatting and error handling are managed globally by filters and middleware.
    /// </remarks>
    [ApiController]
    [Authorize]
    public class ToDoStateController : BaseController
    {
        public ToDoStateController(IMediator mediator, IMapper mapper)
            : base(mediator, mapper)
        {
        }

        /// <summary>
        /// Retrieves all to-do items for the current user.
        /// </summary>
        /// <returns>
        /// A service result containing the list of to-do items.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetToDoListQuery
            {
                UserId = UserId
            };

            var response = await Mediator.Send(query);

            return Ok(response);
        }

        /// <summary>
        /// Retrieves to-do items filtered by status.
        /// </summary>
        /// <param name="status">The status to filter by.</param>
        /// <returns>
        /// A service result containing filtered to-do items.
        /// </returns>
        [HttpGet("by-status")]
        public async Task<IActionResult> GetByStatus([FromQuery] ToDoStatus status)
        {
            var query = new GetToDoListByStatusQuery
            {
                UserId = UserId,
                Status = status
            };

            var response = await Mediator.Send(query);

            return Ok(response);
        }

        /// <summary>
        /// Retrieves to-do items filtered by priority.
        /// </summary>
        /// <param name="priority">The priority to filter by.</param>
        /// <returns>
        /// A service result containing filtered to-do items.
        /// </returns>
        [HttpGet("by-priority")]
        public async Task<IActionResult> GetByPriority([FromQuery] ToDoPriority priority)
        {
            var query = new GetToDoListByPriorityQuery
            {
                UserId = UserId,
                Priority = priority
            };

            var response = await Mediator.Send(query);

            return Ok(response);
        }

        /// <summary>
        /// Retrieves all overdue to-do items for the current user.
        /// </summary>
        /// <returns>
        /// A service result containing overdue to-do items.
        /// </returns>
        [HttpGet("overdue")]
        public async Task<IActionResult> GetAllOverDue()
        {
            var query = new GetToDoListOverdueQuery
            {
                UserId = UserId
            };

            var response = await Mediator.Send(query);

            return Ok(response);
        }     
    }
}