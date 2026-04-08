using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.TaskStateService.Application.Common.Exceptions;
using ToDoList.TaskStateService.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.TaskStateService.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskStateService.Application.Interfaces.Repository;
using ToDoList.TaskStateService.Domain;

namespace ToDoList.TaskStateService.Application.Features.ToDoItems.Commands.ChangeToDoPriority
{
    public class ChangeToDoPriorityCommandHandler 
        : IRequestHandler<ChangeToDoPriorityCommand, 
        ServiceResult<ChangeToDoPriorityResponseDto>>
    {
        public readonly IToDoRepository _repository;
        public ChangeToDoPriorityCommandHandler(IToDoRepository repository) =>
            _repository = repository;
        public async Task<ServiceResult<ChangeToDoPriorityResponseDto>> Handle(
            ChangeToDoPriorityCommand request, 
            CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (entity is null || entity.UserId != request.UserId)
            {
                // logger throw new NotFoundException(nameof(ToDoItem), request.Id);

                return ServiceResult<ChangeToDoPriorityResponseDto>
                    .Fail(ServiceErrorCode.NotFound);
            }
            if (entity.Priority == request.Priority)
            {
                // logger throw new IdenticalReplacementException(nameof(ToDoItem), entity.Priority, entity.Id);

                return ServiceResult<ChangeToDoPriorityResponseDto>
                    .Fail(ServiceErrorCode.ValidationFailed);
            }

            entity.EditDate = DateTime.UtcNow;
            entity.Priority = request.Priority;

            try
            {
                await _repository.UpdateAsync(entity, cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                //logger ex

                return ServiceResult<ChangeToDoPriorityResponseDto>
                    .Fail(ServiceErrorCode.Conflict);
            }

            var response = new ChangeToDoPriorityResponseDto();

            return ServiceResult<ChangeToDoPriorityResponseDto>
                .Success(response);
        }
    }
}