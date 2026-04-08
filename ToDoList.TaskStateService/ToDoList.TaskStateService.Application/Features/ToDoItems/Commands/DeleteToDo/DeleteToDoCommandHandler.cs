using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoList.TaskStateService.Application.Common.Exceptions;
using ToDoList.TaskStateService.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.TaskStateService.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskStateService.Application.Interfaces.Repository;
using ToDoList.TaskStateService.Domain;

namespace ToDoList.TaskStateService.Application.Features.ToDoItems.Commands.DeleteToDo
{
    public class DeleteToDoCommandHandler 
        : IRequestHandler<DeleteToDoCommand, 
        ServiceResult<DeleteToDoResponseDto>>
    {
        public readonly IToDoRepository _repository;
        public DeleteToDoCommandHandler(IToDoRepository repository) =>
            _repository = repository;
        public async Task<ServiceResult<DeleteToDoResponseDto>> Handle(
            DeleteToDoCommand request, 
            CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (entity == null || entity.UserId != request.UserId)
            {
                //logger throw new NotFoundException(nameof(ToDoItem), request.Id);

                return ServiceResult<DeleteToDoResponseDto>
                    .Fail(ServiceErrorCode.NotFound);
            }

            try
            {
                await _repository.DeleteAsync(entity, cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return ServiceResult<DeleteToDoResponseDto>
                    .Fail(ServiceErrorCode.Conflict);
            }

            var response = new DeleteToDoResponseDto();

            return ServiceResult<DeleteToDoResponseDto>
                .Success(response);
        }
    }
}
