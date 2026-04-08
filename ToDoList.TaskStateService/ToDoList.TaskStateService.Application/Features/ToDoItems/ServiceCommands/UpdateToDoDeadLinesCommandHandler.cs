using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ToDoList.TaskStateService.Application.Common.Exceptions;
using ToDoList.TaskStateService.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.TaskStateService.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskStateService.Application.Interfaces.Repository;
using ToDoList.TaskStateService.Domain;
using ToDoList.TaskStateService.Domain.ValueObjects;

namespace ToDoList.TaskStateService.Application.Features.ToDoItems.ServiceCommands
{
    public class UpdateToDoDeadLinesCommandHandler 
        : IRequestHandler<UpdateToDoDeadLinesCommand, 
            InternalServiceResult<UpdateToDoDeadLinesResponseDto>>
    {
        private readonly IToDoRepository _repository;
        public UpdateToDoDeadLinesCommandHandler(IToDoRepository repository)
        {
            _repository = repository;
        }
        public async Task<InternalServiceResult<UpdateToDoDeadLinesResponseDto>> Handle(
            UpdateToDoDeadLinesCommand request, 
            CancellationToken cancellationToken)
        {
            var response = new UpdateToDoDeadLinesResponseDto();

            try
            {
                foreach (var todo in request.Items)
                {
                    var entity = await _repository.GetByIdAsync(todo.Id, cancellationToken);

                    if (entity == null || entity.UserId != todo.UserId)
                    {
                        response.FailUpdate.Add(todo.Id);
                        continue;
                    }

                    if (entity.Status == ToDoStatus.Cancelled)
                    { 
                        response.UpdateRestrictions.Add(todo.Id);
                        continue;
                        }

                    entity.Status = ToDoStatus.Expired;
                    entity.EditDate = DateTime.UtcNow;

                    await _repository.UpdateAsync(entity, cancellationToken);

                    response.SuccessUpdated.Add(entity.Id);
                }

                return InternalServiceResult<UpdateToDoDeadLinesResponseDto>
                    .Success(response);
            }
            catch(Exception ex)
            {
                //logger

                return InternalServiceResult<UpdateToDoDeadLinesResponseDto>
                    .Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}
