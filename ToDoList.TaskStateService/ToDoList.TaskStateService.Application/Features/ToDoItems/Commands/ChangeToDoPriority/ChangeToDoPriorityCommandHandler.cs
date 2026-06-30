using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ToDoList.TaskStateService.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.TaskStateService.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskStateService.Application.Interfaces;
using ToDoList.TaskStateService.Application.Interfaces.Repository;

namespace ToDoList.TaskStateService.Application.Features.ToDoItems.Commands.ChangeToDoPriority
{
    public class ChangeToDoPriorityCommandHandler
        : IRequestHandler<ChangeToDoPriorityCommand,
            ServiceResult<ChangeToDoPriorityResponseDto>>
    {
        private readonly IToDoRepository _repository;
        private readonly IToDoDbContext _context;
        private readonly ILogger _logger;

        public ChangeToDoPriorityCommandHandler(
            IToDoRepository repository,
            IToDoDbContext context,
            ILogger logger)
        {
            _repository = repository;
            _context = context;
            _logger = logger;
        }

        public async Task<ServiceResult<ChangeToDoPriorityResponseDto>> Handle(
            ChangeToDoPriorityCommand request,
            CancellationToken cancellationToken)
        {
            _logger.Information(
                "ChangePriority started. TaskId={TaskId}, UserId={UserId}, Priority={Priority}",
                request.Id, request.UserId, request.Priority);

            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (entity is null || entity.UserId != request.UserId)
            {
                _logger.Warning(
                    "ChangePriority failed: not found or access denied. TaskId={TaskId}, UserId={UserId}",
                    request.Id, request.UserId);

                return ServiceResult<ChangeToDoPriorityResponseDto>
                    .Fail(ServiceErrorCode.NotFound);
            }

            if (entity.Priority == request.Priority)
            {
                _logger.Warning(
                    "ChangePriority skipped: same priority. TaskId={TaskId}, Priority={Priority}",
                    request.Id, request.Priority);

                return ServiceResult<ChangeToDoPriorityResponseDto>
                    .Fail(ServiceErrorCode.ValidationFailed);
            }

            entity.Priority = request.Priority;
            entity.EditDate = DateTime.UtcNow;

            try
            {
                _repository.Update(entity);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.Information(
                    "ChangePriority success. TaskId={TaskId}, Priority={Priority}",
                    request.Id, request.Priority);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.Error(
                    ex,
                    "ChangePriority concurrency conflict. TaskId={TaskId}",
                    request.Id);

                return ServiceResult<ChangeToDoPriorityResponseDto>
                    .Fail(ServiceErrorCode.Conflict);
            }
            catch (Exception ex)
            {
                _logger.Error(
                    ex,
                    "ChangePriority unexpected error. TaskId={TaskId}",
                    request.Id);

                throw;
            }

            return ServiceResult<ChangeToDoPriorityResponseDto>
                .Success(new ChangeToDoPriorityResponseDto());
        }
    }
}