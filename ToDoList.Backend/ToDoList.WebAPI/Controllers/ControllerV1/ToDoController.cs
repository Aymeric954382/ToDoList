using Microsoft.AspNetCore.Authorization;
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
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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
        /// Retrieves a list of to-do items filtered by their status.
        /// </summary>
        /// <remarks>This endpoint requires the user to be authenticated. The returned list is specific to the
        /// authenticated user's to-do items.</remarks>
        /// <param name="status">The status of the to-do items to retrieve. Must be a valid <see cref="ToDoStatus"/> value.</param>
        /// <returns>An <see cref="ActionResult{T}"/> containing a <see cref="ToDoListContainer"/> with the filtered to-do items  if the
        /// request is successful. Returns a 200 OK response on success or a 401 Unauthorized response if the user is not
        /// authenticated.</returns>
        [HttpGet("by-status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ToDoListContainer>> GetByStatus([FromQuery] ToDoStatus status) =>
            Ok(await Mediator.Send(new GetToDoListByStatusQuery { UserId = UserId, Status = status }));

        /// <summary>
        /// Retrieves a to-do list filtered by the specified priority level.
        /// </summary>
        /// <remarks>The to-do list is filtered based on the priority level provided in the query
        /// parameter.  Ensure the user is authenticated before calling this endpoint.</remarks>
        /// <param name="priority">The priority level to filter the to-do list by.</param>
        /// <returns>An <see cref="ActionResult{T}"/> containing a <see cref="ToDoListContainer"/> with the filtered to-do items.
        /// Returns a 200 OK response if the operation is successful, or a 401 Unauthorized response if the user is not
        /// authenticated.</returns>
        [HttpGet("by-priority")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ToDoListContainer>> GetByPriority([FromQuery] ToDoPriority priority) =>
            Ok(await Mediator.Send(new GetToDoListByPriorityQuery { UserId = UserId, Priority = priority }));

        /// <summary>
        /// Retrieves all overdue to-do items for the current user.
        /// </summary>
        /// <remarks>This method returns a container of overdue to-do items associated with the
        /// authenticated user.  The user must be authorized to access this endpoint.</remarks>
        /// <returns>An <see cref="ActionResult{T}"/> containing a <see cref="ToDoListContainer"/> with the overdue to-do items.
        /// Returns an empty container if no overdue items are found.</returns>
        [HttpGet("overdue")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ToDoListContainer>> GetAllOverDue() =>
            Ok(await Mediator.Send(new GetToDoListOverdueQuery { UserId = UserId }));

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
        /// Updates the priority of a to-do item.
        /// </summary>
        /// <remarks>This operation requires the user to be authenticated. If the user is not authorized, 
        /// a 401 Unauthorized response is returned.</remarks>
        /// <param name="dto">An object containing the ID of the to-do item to update, the new priority value,  and any additional data
        /// required for the operation.</param>
        /// <returns>A <see cref="Task{IActionResult}"/> representing the asynchronous operation.  Returns a 204 No Content
        /// response on success.</returns>
        [HttpPut("priority")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

        /// <summary>
        /// Updates the status of a to-do item.
        /// </summary>
        /// <remarks>This method processes a request to change the status of a to-do item. The user must
        /// be authenticated, and the  provided <paramref name="dto"/> must contain valid data. The method does not
        /// return any content upon success.</remarks>
        /// <param name="dto">An object containing the ID of the to-do item, the new status to apply, and any additional required data.</param>
        /// <returns>A <see cref="IActionResult"/> indicating the result of the operation. Returns a 200 OK status if the update
        /// is successful,  or a 401 Unauthorized status if the user is not authorized.</returns>
        [HttpPut("status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

        /// <summary>
        /// Retrieves a message indicating the current user's authorization status.
        /// </summary>
        /// <remarks>This method returns a success response containing a message with the user's
        /// identifier if the user is authenticated. The identifier is extracted from the "sub" claim in the user's
        /// authentication token.</remarks>
        /// <returns>An <see cref="IActionResult"/> containing a JSON object with a message that includes the user's identifier
        /// if authenticated.</returns>
        [HttpGet("all")]
        public IActionResult GetAllUser()
        {
            var userId = User.FindFirst("sub")?.Value;
            return Ok(new { Message = $"Authorized as {userId}" });
        }
    }
}
