using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.ToDoItems.Commands.ChangeToDoContent;
using ToDoList.Application.ToDoItems.Commands.ChangeToDoPriority;
using ToDoList.Application.ToDoItems.Commands.ChangeToDoStatus;
using ToDoList.Application.ToDoItems.Commands.CreateToDoItem;
using ToDoList.Application.ToDoItems.Commands.DeleteToDo;
using ToDoList.Application.ToDoItems.Queries.Containers;
using ToDoList.Application.ToDoItems.Queries.GetByPriority;
using ToDoList.Application.ToDoItems.Queries.GetByStatus;
using ToDoList.Application.ToDoItems.Queries.GetListToDo;
using ToDoList.Application.ToDoItems.Queries.GetOverdueToDos;
using ToDoList.Domain.ToDo.ValueObjects;
using ToDoList.WebAPI.Models;

namespace ToDoList.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ToDoController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<ToDoListContainer>> GetAll() =>
           Ok(await Mediator.Send(new GetToDoListQuery { UserId = UserId }));

        [HttpGet("by-status")]
        public async Task<ActionResult<ToDoListContainer>> GetByStatus([FromQuery] ToDoStatus status) =>
            Ok(await Mediator.Send(new GetToDoListByStatusQuery { UserId = UserId, Status = status }));

        [HttpGet("by-priority")]
        public async Task<ActionResult<ToDoListContainer>> GetByPriority([FromQuery] ToDoPriority priority) =>
            Ok(await Mediator.Send(new GetToDoListByPriorityQuery { UserId = UserId, Priority = priority }));

        [HttpGet("overdue")]
        public async Task<ActionResult<ToDoListContainer>> GetAllOverDue() =>
            Ok(await Mediator.Send(new GetToDoListOverdueQuery { UserId = UserId }));

        [HttpPut("content")]
        public async Task<IActionResult> ChangeContent([FromBody] ChangeToDoContentDto dto)
        {
            var command = new ChangeToDoContentCommand
            {
                Id = dto.Id,
                UserId = UserId,
                Title = dto.Title,
                Details = dto.Details
            };

            await Mediator.Send(command);
            return NoContent();
        }

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
                Title = dto.Title,
                Details = dto.Details,
                DueDate = dto.DueDate,
                Priority = dto.Priority
            };

            var id = await Mediator.Send(command);
            return Ok(id);
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

        [HttpGet("all")]
        public IActionResult GetAllUser()
        {
            var userId = User.FindFirst("sub")?.Value;
            return Ok(new { Message = $"Authorized as {userId}" });
        }
    }
}
