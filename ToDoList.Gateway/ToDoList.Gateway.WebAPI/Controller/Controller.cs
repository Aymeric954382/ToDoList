using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Gateway.Application.ToDoItem.Commands.ChangeToDoPriority;
using ToDoList.Gateway.Application.ToDoItem.Commands.ChangeToDoStatus;
using ToDoList.Gateway.Application.ToDoItem.Commands.CreateToDo;
using ToDoList.Gateway.Application.ToDoItem.Commands.DeleteToDo;
using ToDoList.Gateway.Application.ToDoItem.Queries.Containers;
using ToDoList.Gateway.Application.ToDoItem.Queries.GetByPriority;
using ToDoList.Gateway.Application.ToDoItem.Queries.GetByStatus;
using ToDoList.Gateway.Application.ToDoItem.Queries.GetListToDo;
using ToDoList.Gateway.Application.ToDoItem.Queries.GetOverdueToDo;
using ToDoList.Gateway.Contracts.ApiClients.ValueObjects;
using ToDoList.Gateway.WebAPI.Model.Change;
using ToDoList.Gateway.WebAPI.Model.Create;


namespace ToDoList.Gateway.WebAPI.Controller
{
    [ApiVersion("1.0")]
    public class TaskController : BaseController
    {
        public TaskController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public async Task<ActionResult<ToDoListContainer>> GetAllToDos() =>
            Ok(await Mediator.Send(new GetToDoListQuery { UserId = UserId }));

        [HttpGet("by-status")]
        public async Task<ActionResult<ToDoListContainer>> GetByStatus([FromQuery] ToDoStatus status) =>
            Ok(await Mediator.Send(new GetToDoListByStatusQuery { UserId = UserId, Status = status }));

        [HttpGet("by-priority")]
        public async Task<ActionResult<ToDoListContainer>> GetByPriority([FromQuery] ToDoPriority priority) =>
           Ok(await Mediator.Send(new GetToDoListByPriorityQuery { UserId = UserId, Priority = priority }));

        [HttpGet("overdue")]
        public async Task<ActionResult<ToDoListContainer>> GetAllOverDue() =>
            Ok(await Mediator.Send(new GetToDoListByOverdueQuery { UserId = UserId }));

        [HttpPut("priority")]
        public async Task<IActionResult> ChangePriority([FromBody] ChangeToDoPriorityDto dto)
        {
            var command = new ChangeToDoPriorityCommand
            {
                Id = dto.Id,
                UserId = UserId,
                Priority = dto.Priority
            };

            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut("status")]
        public async Task<IActionResult> ChangeStatus([FromBody] ChangeToDoStatusDto dto)
        {
            var command = new ChangeToDoStatusCommand
            {
                Id = dto.Id,
                UserId = UserId,
                Status = dto.Status
            };

            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateToDoDto dto)
        {
            var command = new CreateToDoCommand
            {
                UserId = UserId,
                DueDate = dto.DueDate,
                Priority = dto.Priority
            };

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteToDoCommand
            {
                Id = id,
                UserId = UserId
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
