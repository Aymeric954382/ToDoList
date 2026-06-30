using MediatR;
using Serilog;
using ToDoList.Gateway.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Interfaces.Orchestartors;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Get.ResponseContainers;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetByStatus
{
    public class GetToDoListByStatusQueryHandler
        : IRequestHandler<GetToDoListByStatusQuery,
            ServiceResult<GetToDoListByStatusResponseDto>>
    {
        private readonly IGetToDoListByStatusOrchestrator _orchestrator;
        private readonly ILogger _logger;

        public GetToDoListByStatusQueryHandler(
            IGetToDoListByStatusOrchestrator orchestrator,
            ILogger logger)
        {
            _orchestrator = orchestrator;
            _logger = logger;
        }

        public async Task<ServiceResult<GetToDoListByStatusResponseDto>> Handle(
            GetToDoListByStatusQuery request,
            CancellationToken cancellationToken)
        {
            _logger.Information("GetToDoListByStatus started");

            try
            {
                var result = await _orchestrator.GetListByStatusAsync(
                    request,
                    cancellationToken);

                _logger.Information("GetToDoListByStatus completed successfully");

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "GetToDoListByStatus failed");

                return ServiceResult<GetToDoListByStatusResponseDto>
                    .Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}