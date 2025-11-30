using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Worker.Application.Common.Exceptions;
using ToDoList.Worker.Application.Interfaces.Repository;
using ToDoList.Worker.Domain;

namespace ToDoList.Worker.Application.ToDoItems.Commands.DeleteToDo
{
    public class DeleteToDoCommandHandler : IRequestHandler<DeleteToDoCommand, Unit>
    {
        public readonly IToDoRepository _repository;
        public DeleteToDoCommandHandler(IToDoRepository repository) =>
            _repository = repository;
        public async Task<Unit> Handle(DeleteToDoCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(ToDoItem), request.Id);
            }

            await _repository.DeleteAsync(entity, cancellationToken);

            return Unit.Value;
        }
    }
}
