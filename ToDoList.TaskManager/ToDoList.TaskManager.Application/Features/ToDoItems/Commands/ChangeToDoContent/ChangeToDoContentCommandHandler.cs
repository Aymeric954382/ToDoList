using MediatR;
using Serilog;
using ToDoList.TaskManager.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.TaskManager.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskManager.Application.Interfaces;
using ToDoList.TaskManager.Application.Interfaces.Repository;

namespace ToDoList.TaskManager.Application.Features.ToDoItems.Commands.ChangeToDoContent
{
    public class ChangeToDoContentCommandHandler
        : IRequestHandler<ChangeToDoContentCommand, ServiceResult<ChangeToDoContentResponseDto>>
    {
        private readonly IToDoRepository _repository;
        private readonly IToDoDbContext _dbContext;
        private readonly ILogger _logger;

        public ChangeToDoContentCommandHandler(
            IToDoRepository repository,
            IToDoDbContext dbContext,
            ILogger logger)
        {
            _repository = repository;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<ServiceResult<ChangeToDoContentResponseDto>> Handle(
            ChangeToDoContentCommand request,
            CancellationToken cancellationToken)
        {
            _logger.Information(
                "ChangeToDoContent started. TaskId={TaskId}, UserId={UserId}",
                request.Id,
                request.UserId);

            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (entity == null || entity.UserId != request.UserId)
            {
                _logger.Warning(
                    "ChangeToDoContent not found or access denied. TaskId={TaskId}, UserId={UserId}",
                    request.Id,
                    request.UserId);

                return ServiceResult<ChangeToDoContentResponseDto>
                    .Fail(ServiceErrorCode.NotFound);
            }

            entity.Title = request.Title;
            entity.Details = request.Details;

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);

                _logger.Information(
                    "ChangeToDoContent completed successfully. TaskId={TaskId}",
                    request.Id);

                return ServiceResult<ChangeToDoContentResponseDto>
                    .Success(new ChangeToDoContentResponseDto());
            }
            catch (Exception ex)
            {
                _logger.Error(
                    ex,
                    "ChangeToDoContent failed. TaskId={TaskId}, UserId={UserId}",
                    request.Id,
                    request.UserId);

                return ServiceResult<ChangeToDoContentResponseDto>
                    .Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}