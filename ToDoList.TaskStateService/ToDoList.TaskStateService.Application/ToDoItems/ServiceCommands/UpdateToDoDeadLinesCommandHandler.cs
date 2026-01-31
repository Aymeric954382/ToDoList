using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.TaskStateService.Application.Common.Exceptions;
using ToDoList.TaskStateService.Application.Interfaces.Repository;
using ToDoList.TaskStateService.Domain;
using ToDoList.TaskStateService.Domain.ValueObjects;

namespace ToDoList.TaskStateService.Application.ToDoItems.ServiceCommands
{
    public class UpdateToDoDeadLinesCommandHandler : IRequestHandler<UpdateToDoDeadLinesCommand, Unit>
    {
        private readonly IToDoRepository _repository;
        public UpdateToDoDeadLinesCommandHandler(IToDoRepository repository)
        {
            _repository = repository;
        }
        public async Task<Unit> Handle(UpdateToDoDeadLinesCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(ToDoItem), request.Id);
            }

            if (entity.DueDate > DateTime.UtcNow)
                return Unit.Value;

            if (entity.Status == ToDoStatus.Expired)
                return Unit.Value;

            entity.Status = ToDoStatus.Expired;
            entity.EditDate = DateTime.UtcNow;

            await _repository.UpdateAsync(entity, cancellationToken);

            return Unit.Value;
        }
    }
}
