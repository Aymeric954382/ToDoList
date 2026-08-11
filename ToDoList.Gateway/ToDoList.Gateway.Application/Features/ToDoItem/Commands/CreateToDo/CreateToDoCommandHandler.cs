using MediatR;
using Serilog;
using ToDoList.Gateway.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.Handlers.CreateToDo;
using ToDoList.Gateway.Application.Interfaces.Orchestartors;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Commands.CreateToDo
{
    public class CreateToDoCommandHandler
        : IRequestHandler<
            CreateToDoCommand,
            ServiceResult<CreateToDoResponseDto>>
    {
        private readonly ICreateToDoWorkflow _workflow;
        private readonly ILogger _logger;

        public CreateToDoCommandHandler(
            ICreateToDoWorkflow orchestrator,
            ILogger logger)
        {
            _workflow = orchestrator;
            _logger = logger;
        }

        public async Task<ServiceResult<CreateToDoResponseDto>> Handle(
            CreateToDoCommand request,
            CancellationToken cancellationToken)
        {
            _logger.Information(
                "CreateToDo started. UserId={UserId}",
                request.UserId);

            try
            {
                await _workflow.CreateAsync(
                    request,
                    cancellationToken);

                _logger.Information(
                    "CreateToDo completed. UserId={UserId}",
                    request.UserId);

                return ServiceResult<CreateToDoResponseDto>
                    .VoidDataSuccess();
            }
            catch (Exception ex)
            {
                _logger.Error(
                    ex,
                    "CreateToDo failed. UserId={UserId}",
                    request.UserId);

                return ServiceResult<CreateToDoResponseDto>
                    .Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}