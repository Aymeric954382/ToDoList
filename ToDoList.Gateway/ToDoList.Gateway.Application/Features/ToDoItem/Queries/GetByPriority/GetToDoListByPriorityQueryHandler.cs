using MediatR;
using Serilog;
using ToDoList.Gateway.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Interfaces.Orchestartors;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Get.ResponseContainers;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Queries.Handlers.GetByPriority
{
    public class GetToDoListByPriorityQueryHandler
        : IRequestHandler<GetToDoListByPriorityQuery,
            ServiceResult<GetToDoListByPriorityResponseDto>>
    {
        private readonly IGetToDoListByPriorityOrchestrator _orchestrator;
        private readonly ILogger _logger;

        public GetToDoListByPriorityQueryHandler(
            IGetToDoListByPriorityOrchestrator orchestrator,
            ILogger logger)
        {
            _orchestrator = orchestrator;
            _logger = logger;
        }

        public async Task<ServiceResult<GetToDoListByPriorityResponseDto>> Handle(
            GetToDoListByPriorityQuery request,
            CancellationToken cancellationToken)
        {
            _logger.Information("GetToDoListByPriority started");

            try
            {
                var result = await _orchestrator.GetListByPriorityAsync(
                    request,
                    cancellationToken);

                _logger.Information("GetToDoListByPriority completed successfully");

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "GetToDoListByPriority failed");

                return ServiceResult<GetToDoListByPriorityResponseDto>
                    .Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}