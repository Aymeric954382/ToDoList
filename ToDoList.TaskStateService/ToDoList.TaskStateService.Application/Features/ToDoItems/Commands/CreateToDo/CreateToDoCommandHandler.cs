using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using StackExchange.Redis;
using ToDoList.TaskStateService.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.TaskStateService.Application.Common.Stubs;
using ToDoList.TaskStateService.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskStateService.Application.Interfaces;
using ToDoList.TaskStateService.Application.Interfaces.Redis;
using ToDoList.TaskStateService.Application.Interfaces.Repository;
using ToDoList.TaskStateService.Domain;
using ToDoList.TaskStateService.Domain.ValueObjects;

namespace ToDoList.TaskStateService.Application.Features.ToDoItems.Commands.CreateToDo
{
    public class CreateToDoCommandHandler
        : IRequestHandler<CreateToDoCommand,
            ServiceResult<CreateToDoResponseDto>>
    {
        private readonly IToDoRepository _repository;
        private readonly IDeadLineQueue _redis;
        private readonly ILogger _logger;
        private readonly IToDoDbContext _context;

        public CreateToDoCommandHandler(
            IToDoRepository repository,
            IDeadLineQueue redis,
            ILogger logger,
            IToDoDbContext context)
        {
            _repository = repository;
            _redis = redis;
            _logger = logger;
            _context = context;
        }

        public async Task<ServiceResult<CreateToDoResponseDto>> Handle(
            CreateToDoCommand request,
            CancellationToken cancellationToken)
        {
            _logger.Information(
                "CreateToDo started. TaskId={TaskId}, UserId={UserId}, DueDate={DueDate}, Priority={Priority}",
                request.Id, request.UserId, request.DueDate, request.Priority);

            var toDoItem = new ToDoItem
            {
                UserId = request.UserId,
                Id = request.Id,
                Status = ToDoStatus.Active,
                DueDate = request.DueDate,
                CreationDate = DateTime.UtcNow,
                EditDate = null,
                Priority = request.Priority
            };

            try
            {
                _repository.Add(toDoItem);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.Information(
                    "CreateToDo saved successfully. TaskId={TaskId}",
                    toDoItem.Id);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.Error(
                    ex,
                    "CreateToDo DB concurrency conflict. TaskId={TaskId}",
                    toDoItem.Id);

                return ServiceResult<CreateToDoResponseDto>
                    .Fail(ServiceErrorCode.Conflict);
            }
            catch (Exception ex)
            {
                _logger.Error(
                    ex,
                    "CreateToDo unexpected DB error. TaskId={TaskId}",
                    toDoItem.Id);

                throw;
            }

            try
            {
                if (toDoItem.DueDate is DateTime dueDate)
                {
                    var task = new DeadLineStub
                    {
                        TaskId = toDoItem.Id,
                        UserId = toDoItem.UserId,
                        DeadlineUnix = new DateTimeOffset(dueDate)
                            .ToUnixTimeSeconds()
                    };

                    await _redis.AddDeadlineStubAsync(task);

                    _logger.Information(
                        "Deadline queued successfully. TaskId={TaskId}",
                        toDoItem.Id);
                }
            }
            catch (RedisException ex)
            {
                _logger.Warning(
                    ex,
                    "Failed to enqueue deadline task. TaskId={TaskId}",
                    toDoItem.Id);
            }

            return ServiceResult<CreateToDoResponseDto>
                .Success(new CreateToDoResponseDto());
        }
    }
}