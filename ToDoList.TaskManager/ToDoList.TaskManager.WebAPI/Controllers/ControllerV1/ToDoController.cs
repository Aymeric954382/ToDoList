using Microsoft.AspNetCore.Mvc;
using ToDoList.TaskManager.Application.Features.ToDoItems.Commands.ChangeToDoContent;
using ToDoList.TaskManager.Application.Features.ToDoItems.Commands.CreateToDo;
using ToDoList.TaskManager.Application.Features.ToDoItems.Commands.DeleteToDo;
using ToDoList.TaskManager.Application.Features.ToDoItems.Queries.GetListToDo;
using ToDoList.TaskManager.WebAPI.Models.Change;
using ToDoList.TaskManager.WebAPI.Models.Create;
using ToDoList.TaskManager.WebAPI.Models.Delete;
using ToDoList.TaskManager.WebAPI.Models.Get;

namespace ToDoList.TaskManager.WebAPI.Controllers.ControllerV1
{
    [ApiVersion("1.0")]
    public class ToDoController : BaseController
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<GetToDoListDto>> GetAll() =>
           Ok(await Mediator.Send(new GetToDoListQuery { UserId = UserId }));


        [HttpPut("content")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ChangeToDoContentDto>> ChangeContent([FromBody] ChangeToDoContentDto dto)
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CreateToDoDto>> Create([FromBody] CreateToDoDto dto)
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<DeleteToDoDto>> Delete([FromBody] DeleteToDoDto dto)
        {
            var command = new DeleteToDoCommand
            {
                Id = dto.Id,
                UserId = UserId
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
