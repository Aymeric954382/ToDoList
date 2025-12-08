using Microsoft.AspNetCore.Mvc;
using ToDoList.TaskManager.Application.ToDoItems.Commands.ChangeToDoContent;
using ToDoList.TaskManager.Application.ToDoItems.Commands.CreateToDo;
using ToDoList.TaskManager.Application.ToDoItems.Commands.DeleteToDo;
using ToDoList.TaskManager.Application.ToDoItems.Queries.Containers;
using ToDoList.TaskManager.Application.ToDoItems.Queries.GetListToDo;
using ToDoList.TaskManager.Domain.ValueObjects;
using ToDoList.TaskManager.WebAPI.Models;

namespace ToDoList.TaskManager.WebAPI.Controllers.ControllerV1
{
    [ApiVersion("1.0")]
    public class ToDoController : BaseController
    {
        /// <summary>
        /// Retrieves all to-do lists associated with the current user.
        /// </summary>
        /// <remarks>This method returns a container object that includes all to-do lists for the
        /// authenticated user.  The user must be authenticated to access this endpoint.</remarks>
        /// <returns>An <see cref="ActionResult{T}"/> containing a <see cref="ToDoListContainer"/> with the user's to-do lists.
        /// Returns a 200 OK status code if the operation is successful.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ToDoListContainer>> GetAll() =>
           Ok(await Mediator.Send(new GetToDoListQuery { UserId = UserId }));

        /// <summary>
        /// Updates the content of an existing to-do item.
        /// </summary>
        /// <remarks>This operation requires the user to be authenticated. If the user is not authorized,
        /// a <see cref="StatusCodes.Status401Unauthorized"/> response is returned.</remarks>
        /// <param name="dto">An object containing the updated content details for the to-do item. The <see
        /// cref="ChangeToDoContentDto.Id"/> property specifies the ID of the to-do item to update, and the <see
        /// cref="ChangeToDoContentDto.Title"/> and <see cref="ChangeToDoContentDto.Details"/> properties provide the
        /// new content.</param>
        /// <returns>A <see cref="NoContentResult"/> indicating that the update was successful, or an appropriate HTTP status
        /// code if the operation fails.</returns>
        [HttpPut("content")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

        /// <summary>
        /// Creates a new to-do item based on the provided data.
        /// </summary>
        /// <remarks>This method requires the user to be authenticated. If the user is not authenticated, 
        /// a 401 Unauthorized response is returned. On success, a 200 OK response is returned  with the unique
        /// identifier of the created to-do item.</remarks>
        /// <param name="dto">The data transfer object containing the details of the to-do item to create.</param>
        /// <returns>The unique identifier of the newly created to-do item.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

        /// <summary>
        /// Deletes a to-do item identified by the specified ID.
        /// </summary>
        /// <remarks>This operation requires the user to be authenticated. If the user is not authorized, 
        /// the response will be an HTTP 401 Unauthorized status.</remarks>
        /// <param name="id">The unique identifier of the to-do item to delete.</param>
        /// <returns>A <see cref="NoContentResult"> (HTTP 204) if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
