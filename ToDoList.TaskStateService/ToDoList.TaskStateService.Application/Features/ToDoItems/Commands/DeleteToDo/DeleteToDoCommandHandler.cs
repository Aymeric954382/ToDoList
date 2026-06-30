using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ToDoList.TaskStateService.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.TaskStateService.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskStateService.Application.Interfaces;
using ToDoList.TaskStateService.Application.Interfaces.Repository;
using ToDoList.TaskStateService.Domain;

namespace ToDoList.TaskStateService.Application.Features.ToDoItems.Commands.DeleteToDo
{
    public class DeleteToDoCommandHandler
        : IRequestHandler<DeleteToDoCommand,
            ServiceResult<DeleteToDoResponseDto>>
    {
        private readonly IToDoRepository _repository;
        private readonly ILogger _logger;
        private readonly IToDoDbContext _context;

        public DeleteToDoCommandHandler(
            IToDoRepository repository,
            IToDoDbContext context,
            ILogger logger)
        {
            _repository = repository;
            _context = context;
            _logger = logger;
        }

        public async Task<ServiceResult<DeleteToDoResponseDto>> Handle(
            DeleteToDoCommand request,
            CancellationToken cancellationToken)
        {
            _logger.Information(
                "DeleteToDo started. TaskId={TaskId}, UserId={UserId}",
                request.Id, request.UserId);

            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (entity is null || entity.UserId != request.UserId)
            {
                _logger.Warning(
                    "DeleteToDo failed: not found or access denied. TaskId={TaskId}, UserId={UserId}",
                    request.Id, request.UserId);

                return ServiceResult<DeleteToDoResponseDto>
                    .Fail(ServiceErrorCode.NotFound);
            }

            try
            {
                _repository.Delete(entity);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.Information(
                    "DeleteToDo success. TaskId={TaskId}",
                    request.Id);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.Error(
                    ex,
                    "DeleteToDo concurrency conflict. TaskId={TaskId}",
                    request.Id);

                return ServiceResult<DeleteToDoResponseDto>
                    .Fail(ServiceErrorCode.Conflict);
            }
            catch (Exception ex)
            {
                _logger.Error(
                    ex,
                    "DeleteToDo unexpected error. TaskId={TaskId}",
                    request.Id);

                throw;
            }

            return ServiceResult<DeleteToDoResponseDto>
                .Success(new DeleteToDoResponseDto());
        }
    }
}