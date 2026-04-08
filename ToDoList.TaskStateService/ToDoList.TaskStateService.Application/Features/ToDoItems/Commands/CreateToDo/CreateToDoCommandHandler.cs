using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoList.TaskStateService.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.TaskStateService.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskStateService.Application.Interfaces.Repository;
using ToDoList.TaskStateService.Domain;
using ToDoList.TaskStateService.Domain.ValueObjects;

namespace ToDoList.TaskStateService.Application.Features.ToDoItems.Commands.CreateToDo
{
    public class CreateToDoCommandHandler 
        : IRequestHandler<CreateToDoCommand, 
            ServiceResult<CreateToDoResponseDto>>
    {
        private readonly IToDoRepository _repository;
        public CreateToDoCommandHandler(IToDoRepository repository) =>
            _repository = repository;
        public async Task<ServiceResult<CreateToDoResponseDto>> Handle(
            CreateToDoCommand request, 
            CancellationToken cancellationToken)
        {
            var toDoItem = new ToDoItem
            {
                UserId = request.UserId,
                Id = request.Id,
                Status = ToDoStatus.Active,
                DueDate = request.DueDate,
                CreationDate = DateTime.UtcNow,
                EditDate = null,
                Priority = request.Priority
            };

            try
            {
                await _repository.AddAsync(toDoItem, cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return ServiceResult<CreateToDoResponseDto>
                    .Fail(ServiceErrorCode.Conflict);
            }

            var response = new CreateToDoResponseDto();

            return ServiceResult<CreateToDoResponseDto>
                .Success(response);
        }
    }
}
