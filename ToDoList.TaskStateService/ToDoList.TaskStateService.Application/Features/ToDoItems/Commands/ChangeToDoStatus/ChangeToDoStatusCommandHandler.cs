using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ToDoList.TaskStateService.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.TaskStateService.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskStateService.Application.Interfaces;
using ToDoList.TaskStateService.Application.Interfaces.Repository;

namespace ToDoList.TaskStateService.Application.Features.ToDoItems.Commands.ChangeToDoStatus
{
    public class ChangeToDoStatusCommandHandler
        : IRequestHandler<ChangeToDoStatusCommand,
            ServiceResult<ChangeToDoStatusResponseDto>>
    {
        private readonly IToDoRepository _repository;
        private readonly IToDoDbContext _context;
        private readonly ILogger _logger;

        public ChangeToDoStatusCommandHandler(
            IToDoRepository repository,
            IToDoDbContext context,
            ILogger logger)
        {
            _repository = repository;
            _context = context;
            _logger = logger;
        }

        public async Task<ServiceResult<ChangeToDoStatusResponseDto>> Handle(
            ChangeToDoStatusCommand request,
            CancellationToken cancellationToken)
        {
            _logger.Information(
                "ChangeStatus started. TaskId={TaskId}, UserId={UserId}, NewStatus={Status}",
                request.Id, request.UserId, request.Status);

            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (entity is null || entity.UserId != request.UserId)
            {
                _logger.Warning(
                    "ChangeStatus failed: not found or access denied. TaskId={TaskId}, UserId={UserId}",
                    request.Id, request.UserId);

                return ServiceResult<ChangeToDoStatusResponseDto>
                    .Fail(ServiceErrorCode.NotFound);
            }

            if (entity.Status == request.Status)
            {
                _logger.Warning(
                    "ChangeStatus skipped: same status. TaskId={TaskId}, Status={Status}",
                    request.Id, request.Status);

                return ServiceResult<ChangeToDoStatusResponseDto>
                    .Fail(ServiceErrorCode.ValidationFailed);
            }

            entity.Status = request.Status;
            entity.EditDate = DateTime.UtcNow;

            try
            {
                _repository.Update(entity);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.Information(
                    "ChangeStatus success. TaskId={TaskId}, NewStatus={Status}",
                    request.Id, request.Status);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.Error(
                    ex,
                    "ChangeStatus concurrency conflict. TaskId={TaskId}",
                    request.Id);

                return ServiceResult<ChangeToDoStatusResponseDto>
                    .Fail(ServiceErrorCode.Conflict);
            }
            catch (Exception ex)
            {
                _logger.Error(
                    ex,
                    "ChangeStatus unexpected error. TaskId={TaskId}",
                    request.Id);

                throw;
            }

            return ServiceResult<ChangeToDoStatusResponseDto>
                .Success(new ChangeToDoStatusResponseDto());
        }
    }
}