using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ToDoList.TaskStateService.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.TaskStateService.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskStateService.Application.Interfaces;
using ToDoList.TaskStateService.Application.Interfaces.Repository;

namespace ToDoList.TaskStateService.Application.Features.ToDoItems.Commands.ChangeToDoDueDate
{
    public class ChangeToDoDueDateCommandHandler
        : IRequestHandler<ChangeToDoDueDateCommand,
            ServiceResult<ChangeToDoDueDateResponseDto>>
    {
        private readonly IToDoRepository _repository;
        private readonly IToDoDbContext _context;
        private readonly ILogger _logger;

        public ChangeToDoDueDateCommandHandler(
            IToDoRepository repository,
            ILogger logger,
            IToDoDbContext context)
        {
            _repository = repository;
            _context = context;
            _logger = logger;
        }

        public async Task<ServiceResult<ChangeToDoDueDateResponseDto>> Handle(
            ChangeToDoDueDateCommand request,
            CancellationToken cancellationToken)
        {
            _logger.Information(
                "ChangeDueDate started. TaskId={TaskId}, UserId={UserId}, NewDueDate={DueDate}",
                request.Id, request.UserId, request.DueDate);

            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (entity is null || entity.UserId != request.UserId)
            {
                _logger.Warning(
                    "Task not found or access denied. TaskId={TaskId}, UserId={UserId}",
                    request.Id, request.UserId);

                return ServiceResult<ChangeToDoDueDateResponseDto>
                    .Fail(ServiceErrorCode.NotFound);
            }

            if (request.DueDate == entity.DueDate)
            {
                _logger.Warning(
                    "DueDate not changed (idempotent request). TaskId={TaskId}, DueDate={DueDate}",
                    request.Id, request.DueDate);

                return ServiceResult<ChangeToDoDueDateResponseDto>
                    .Fail(ServiceErrorCode.ValidationFailed);
            }

            entity.EditDate = DateTime.UtcNow;
            entity.DueDate = request.DueDate;

            try
            {
                _repository.Update(entity);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.Information(
                    "DueDate updated successfully. TaskId={TaskId}",
                    request.Id);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.Error(
                    ex,
                    "Concurrency conflict while updating DueDate. TaskId={TaskId}",
                    request.Id);

                return ServiceResult<ChangeToDoDueDateResponseDto>
                    .Fail(ServiceErrorCode.Conflict);
            }

            return ServiceResult<ChangeToDoDueDateResponseDto>
                .Success(new ChangeToDoDueDateResponseDto());
        }
    }
}