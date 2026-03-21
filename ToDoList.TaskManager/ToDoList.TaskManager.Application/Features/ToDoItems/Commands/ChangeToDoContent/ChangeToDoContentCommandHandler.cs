using MediatR;
using ToDoList.TaskManager.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.TaskManager.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskManager.Application.Interfaces.Repository;

namespace ToDoList.TaskManager.Application.Features.ToDoItems.Commands.ChangeToDoContent
{
    public class ChangeToDoContentCommandHandler : IRequestHandler<ChangeToDoContentCommand, ServiceResult<ChangeToDoContentResponseDto>>
    {
        private readonly IToDoRepository _repository;

        public ChangeToDoContentCommandHandler(IToDoRepository repository) =>
            _repository = repository;

        public async Task<ServiceResult<ChangeToDoContentResponseDto>> Handle(
            ChangeToDoContentCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (entity == null || entity.UserId != request.UserId)
            {
                // logger throw new NotFoundException(nameof(ToDoItem), request.Id);

                return ServiceResult<ChangeToDoContentResponseDto>.Fail(ServiceErrorCode.NotFound);
            }

            entity.Title = request.Title;
            entity.Details = request.Details;

            try
            {
                await _repository.UpdateAsync(entity, cancellationToken);

                return ServiceResult<ChangeToDoContentResponseDto>.Success(new ChangeToDoContentResponseDto());
            }
            catch (Exception ex)
            {
                //logger

                return ServiceResult<ChangeToDoContentResponseDto>.Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}
