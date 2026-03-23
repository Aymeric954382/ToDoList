using MediatR;
using ToDoList.TaskManager.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.TaskManager.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskManager.Application.Interfaces.Repository;
using ToDoList.TaskManager.Domain;

namespace ToDoList.TaskManager.Application.Features.ToDoItems.Commands.CreateToDo
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
                Id = Guid.NewGuid(),
                Title = request.Title,
                Details = request.Details,
            };

            try
            {
                await _repository.AddAsync(toDoItem, cancellationToken);

                return ServiceResult<CreateToDoResponseDto>.Success(new CreateToDoResponseDto());
            }
            catch (Exception ex)
            {
                //logger

                return ServiceResult<CreateToDoResponseDto>.Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}
