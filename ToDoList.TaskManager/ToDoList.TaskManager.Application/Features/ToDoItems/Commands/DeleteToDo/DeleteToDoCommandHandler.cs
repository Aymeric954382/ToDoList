using MediatR;
using ToDoList.TaskManager.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.TaskManager.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskManager.Application.Interfaces.Repository;

namespace ToDoList.TaskManager.Application.Features.ToDoItems.Commands.DeleteToDo
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
                return ServiceResult<DeleteToDoResponseDto>.Fail(ServiceErrorCode.NotFound);
            }

            try 
            {
                await _repository.DeleteAsync(entity, cancellationToken);

                return ServiceResult<DeleteToDoResponseDto>.Success(new DeleteToDoResponseDto());
            }
            catch(Exception ex)
            {
                //logger 

                return ServiceResult<DeleteToDoResponseDto>.Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}
