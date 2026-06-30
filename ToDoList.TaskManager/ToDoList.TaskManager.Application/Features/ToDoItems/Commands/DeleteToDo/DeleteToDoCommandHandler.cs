using MediatR;
using Serilog;
using ToDoList.TaskManager.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.TaskManager.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskManager.Application.Interfaces;
using ToDoList.TaskManager.Application.Interfaces.Repository;

namespace ToDoList.TaskManager.Application.Features.ToDoItems.Commands.DeleteToDo
{
    public class DeleteToDoCommandHandler
        : IRequestHandler<DeleteToDoCommand, ServiceResult<DeleteToDoResponseDto>>
    {
        private readonly IToDoRepository _repository;
        private readonly IToDoDbContext _dbContext;
        private readonly ILogger _logger;

        public DeleteToDoCommandHandler(
            IToDoRepository repository,
            IToDoDbContext dbContext,
            ILogger logger)
        {
            _repository = repository;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<ServiceResult<DeleteToDoResponseDto>> Handle(
            DeleteToDoCommand request,
            CancellationToken cancellationToken)
        {
            _logger.Information(
                "DeleteToDo started. TaskId={TaskId}, UserId={UserId}",
                request.Id,
                request.UserId);

            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (entity == null || entity.UserId != request.UserId)
            {
                _logger.Warning(
                    "DeleteToDo not found or access denied. TaskId={TaskId}, UserId={UserId}",
                    request.Id,
                    request.UserId);

                return ServiceResult<DeleteToDoResponseDto>
                    .Fail(ServiceErrorCode.NotFound);
            }

            try
            {
                _repository.Delete(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);

                _logger.Information(
                    "DeleteToDo completed successfully. TaskId={TaskId}",
                    request.Id);

                return ServiceResult<DeleteToDoResponseDto>
                    .Success(new DeleteToDoResponseDto());
            }
            catch (Exception ex)
            {
                _logger.Error(
                    ex,
                    "DeleteToDo failed. TaskId={TaskId}, UserId={UserId}",
                    request.Id,
                    request.UserId);

                return ServiceResult<DeleteToDoResponseDto>
                    .Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}