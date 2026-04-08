using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using ToDoList.TaskStateService.Application.Common.Exceptions;
using ToDoList.TaskStateService.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.TaskStateService.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskStateService.Application.Interfaces.Repository;
using ToDoList.TaskStateService.Domain;

namespace ToDoList.TaskStateService.Application.Features.ToDoItems.Commands.ChangeToDoStatus
{
    public class ChangeToDoStatusCommandHandler 
        : IRequestHandler<ChangeToDoStatusCommand, 
            ServiceResult<ChangeToDoStatusResponseDto>>
    {
        public readonly IToDoRepository _repository;

        public ChangeToDoStatusCommandHandler(IToDoRepository repository) =>
            _repository = repository;
        public async Task<ServiceResult<ChangeToDoStatusResponseDto>> Handle(
            ChangeToDoStatusCommand request, 
            CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (entity == null || entity.UserId != request.UserId)
            {
                //logger throw new NotFoundException(nameof(ToDoItem), request.Id);

                return ServiceResult<ChangeToDoStatusResponseDto>
                    .Fail(ServiceErrorCode.NotFound);
            }
            if (entity.Status == request.Status)
            {
                //logger throw new IdenticalReplacementException(nameof(ToDoItem), entity.Status, entity.Id);

                return ServiceResult<ChangeToDoStatusResponseDto>
                    .Fail(ServiceErrorCode.ValidationFailed);
            }

            entity.Status = request.Status;
            entity.EditDate = DateTime.UtcNow;

            try
            {
                await _repository.UpdateAsync(entity, cancellationToken);
            }
            catch(DbUpdateConcurrencyException)
            {
                return ServiceResult<ChangeToDoStatusResponseDto>
                    .Fail(ServiceErrorCode.Conflict);
            }

            var response = new ChangeToDoStatusResponseDto();

            return ServiceResult<ChangeToDoStatusResponseDto>
                .Success(response);
        }
    }
}
