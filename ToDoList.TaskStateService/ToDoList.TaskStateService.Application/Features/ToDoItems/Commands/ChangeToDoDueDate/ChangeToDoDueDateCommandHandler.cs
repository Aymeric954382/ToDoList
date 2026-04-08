using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoList.TaskStateService.Application.Common.Exceptions;
using ToDoList.TaskStateService.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.TaskStateService.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskStateService.Application.Interfaces.Repository;
using ToDoList.TaskStateService.Domain;

namespace ToDoList.TaskStateService.Application.Features.ToDoItems.Commands.ChangeToDoDueDate
{
    public class ChangeToDoDueDateCommandHandler 
        : IRequestHandler<ChangeToDoDueDateCommand, 
        ServiceResult<ChangeToDoDueDateResponseDto>>
    {
        public readonly IToDoRepository _repository;
        public ChangeToDoDueDateCommandHandler(IToDoRepository repository) =>
            _repository = repository;
        public async Task<ServiceResult<ChangeToDoDueDateResponseDto>> Handle(
            ChangeToDoDueDateCommand request, 
            CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (entity is null || entity.UserId == request.Id)
            {
                // logger throw new NotFoundException(nameof(ToDoItems), request.Id);

                return ServiceResult<ChangeToDoDueDateResponseDto>.Fail(
                    ServiceErrorCode.NotFound);
            }
            if (request.DueDate == entity.DueDate)
            {
                // logger throw new IdenticalReplacementException(nameof(ToDoItem), request.DueDate, request.Id);

                return ServiceResult<ChangeToDoDueDateResponseDto>.Fail(
                    ServiceErrorCode.ValidationFailed);
            }

            entity.EditDate = DateTime.UtcNow;
            entity.DueDate = request.DueDate;

            try
            {
                await _repository.UpdateAsync(entity, cancellationToken);
            }
            catch(DbUpdateConcurrencyException)
            {
                //logger ex

                return ServiceResult<ChangeToDoDueDateResponseDto>.Fail(
                    ServiceErrorCode.Conflict);
            }

            var response = new ChangeToDoDueDateResponseDto(); // return something result when will be need 

            return ServiceResult<ChangeToDoDueDateResponseDto>
                .Success(response);
        }
    }
}
