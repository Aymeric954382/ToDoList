using MediatR;
using Serilog;
using ToDoList.Gateway.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Interfaces.Orchestartors;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Commands.Handlers.CreateToDo
{
    public class CreateToDoCommandHandler
        : IRequestHandler<
            CreateToDoCommand,
            ServiceResult<CreateToDoResponseDto>>
    {
        private readonly ICreateToDoOrchestrator _orchestrator;
        private readonly ILogger _logger;

        public CreateToDoCommandHandler(
            ICreateToDoOrchestrator orchestrator,
            ILogger logger)
        {
            _orchestrator = orchestrator;
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
                var result = await _orchestrator.CreateAsync(
                    request,
                    cancellationToken);

                _logger.Information(
                    "CreateToDo completed. UserId={UserId}",
                    request.UserId);

                return result;
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