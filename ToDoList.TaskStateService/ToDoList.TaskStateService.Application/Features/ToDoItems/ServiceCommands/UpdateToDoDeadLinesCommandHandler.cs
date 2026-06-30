using MediatR;
using Serilog;
using ToDoList.TaskStateService.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.TaskStateService.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskStateService.Application.Interfaces;
using ToDoList.TaskStateService.Application.Interfaces.Repository;
using ToDoList.TaskStateService.Domain.ValueObjects;

namespace ToDoList.TaskStateService.Application.Features.ToDoItems.ServiceCommands
{
    public class UpdateToDoDeadLinesCommandHandler
        : IRequestHandler<UpdateToDoDeadLinesCommand,
            InternalServiceResult<UpdateToDoDeadLinesResponseDto>>
    {
        private readonly IToDoRepository _repository;
        private readonly IToDoDbContext _context;
        private readonly ILogger _logger;

        public UpdateToDoDeadLinesCommandHandler(
            IToDoRepository repository,
            IToDoDbContext context,
            ILogger logger)
        {
            _repository = repository;
            _context = context;
            _logger = logger;
        }

        public async Task<InternalServiceResult<UpdateToDoDeadLinesResponseDto>> Handle(
            UpdateToDoDeadLinesCommand request,
            CancellationToken cancellationToken)
        {
            _logger.Information(
                "UpdateDeadLines started. ItemsCount={Count}",
                request.Items.Count());

            var response = new UpdateToDoDeadLinesResponseDto();

            var ids = request.Items
                .Select(x => x.Id)
                .ToList();

            try
            {
                var entities = await _repository
                    .GetByIdsAsync(ids, cancellationToken);

                var entityMap = entities.ToDictionary(x => x.Id);

                foreach (var todo in request.Items)
                {
                    if (!entityMap.TryGetValue(todo.Id, out var entity)
                        || entity.UserId != todo.UserId)
                    {
                        _logger.Warning(
                            "Deadline update failed. Task not found or invalid user. TaskId={TaskId}, UserId={UserId}",
                            todo.Id,
                            todo.UserId);

                        response.FailUpdate.Add(todo.Id);
                        continue;
                    }

                    if (entity.Status != ToDoStatus.Active)
                    {
                        _logger.Warning(
                            "Deadline update restricted. TaskId={TaskId}, Status={Status}",
                            entity.Id,
                            entity.Status);

                        response.UpdateRestrictions.Add(todo.Id);
                        continue;
                    }

                    entity.Status = ToDoStatus.Expired;
                    entity.EditDate = DateTime.UtcNow;

                    response.SuccessUpdated.Add(entity.Id);
                }

                await _context.SaveChangesAsync(cancellationToken);

                _logger.Information(
                    "UpdateDeadLines completed. Success={SuccessCount}, Failed={FailedCount}, Restricted={RestrictedCount}",
                    response.SuccessUpdated.Count,
                    response.FailUpdate.Count,
                    response.UpdateRestrictions.Count);

                return InternalServiceResult<UpdateToDoDeadLinesResponseDto>
                    .Success(response);
            }
            catch (Exception ex)
            {
                _logger.Error(
                    ex,
                    "UpdateDeadLines failed");

                return InternalServiceResult<UpdateToDoDeadLinesResponseDto>
                    .Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}