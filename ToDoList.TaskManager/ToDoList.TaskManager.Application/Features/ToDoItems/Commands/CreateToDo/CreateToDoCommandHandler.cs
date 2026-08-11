using MediatR;
using Serilog;
using ToDoList.TaskManager.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.TaskManager.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskManager.Application.Interfaces;
using ToDoList.TaskManager.Application.Interfaces.Repository;
using ToDoList.TaskManager.Domain;

namespace ToDoList.TaskManager.Application.Features.ToDoItems.Commands.CreateToDo
{
    public class CreateToDoCommandHandler
        : IRequestHandler<CreateToDoCommand, ServiceResult<CreateToDoResponseDto>>
    {
        private readonly IToDoRepository _repository;
        private readonly IToDoDbContext _dbContext;
        private readonly ILogger _logger;

        public CreateToDoCommandHandler(
            IToDoRepository repository,
            IToDoDbContext dbContext,
            ILogger logger)
        {
            _repository = repository;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<ServiceResult<CreateToDoResponseDto>> Handle(
            CreateToDoCommand request,
            CancellationToken cancellationToken)
        {
            _logger.Information(
                "CreateToDo started. UserId={UserId}",
                request.UserId);

            var toDoItem = new ToDoItem
            {
                Id = request.Id,
                UserId = request.UserId,
                Title = request.Title,
                Details = request.Details,
            };

            try
            {
                _repository.Add(toDoItem);
                await _dbContext.SaveChangesAsync(cancellationToken);

                _logger.Information(
                    "CreateToDo completed successfully. TaskId={TaskId}",
                    toDoItem.Id);

                return ServiceResult<CreateToDoResponseDto>
                    .Success(new CreateToDoResponseDto
                    {
                        Id = toDoItem.Id
                    });
            }
            catch (Exception ex)
            {
                _logger.Error(
                    ex,
                    "CreateToDo failed. UserId={UserId}",
                    request.UserId);

                return ServiceResult<CreateToDoResponseDto>
                    .Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}