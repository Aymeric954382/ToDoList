using Serilog;
using ToDoList.Gateway.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Features.ToDoItem.Aggregation;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.DomainResponseDtos;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetByStatus;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.ServiceQueries.GetByIds;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using ToDoList.Gateway.Application.Interfaces.Orchestartors;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Get;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Get;

namespace ToDoList.Gateway.Application.Features.Orchestrators.QueriesOrchestrators
{
    public class GetByStatusOrchestrator : IGetToDoListByStatusOrchestrator
    {
        private readonly ITaskManagerApiClientAdapter _taskManagerApiClientAdapter;
        private readonly ITaskStateServiceApiClientAdapter _taskStateServiceApiClientAdapter;
        private readonly ILogger _logger;

        public GetByStatusOrchestrator(
            ITaskManagerApiClientAdapter taskManagerApiClientAdapter,
            ITaskStateServiceApiClientAdapter taskStateServiceApiClientAdapter,
            ILogger logger)
        {
            _taskManagerApiClientAdapter = taskManagerApiClientAdapter;
            _taskStateServiceApiClientAdapter = taskStateServiceApiClientAdapter;
            _logger = logger;
        }

        public async Task<ServiceResult<GetToDoListByStatusResponseDto>> GetListByStatusAsync(
            GetToDoListByStatusQuery query,
            CancellationToken cancellationToken)
        {
            _logger.Information("GetByStatus orchestration started. UserId={UserId}", query.UserId);

            try
            {
                var stateResult = await _taskStateServiceApiClientAdapter
                    .GetToDoListByStatusAsync(query, cancellationToken);

                _logger.Information(
                    "StateService returned {Count} items",
                    stateResult?.Items?.Count() ?? 0);

                var ids = stateResult.Items.Select(x => x.Id);

                var getByIdQuery = new GetToDoListByIdsRequestQuery()
                {
                    Ids = ids,
                    UserId = query.UserId
                };

                var managerResult = await _taskManagerApiClientAdapter
                    .GetToDoListByIdAsync(getByIdQuery, cancellationToken);

                _logger.Information(
                    "ManagerService returned {Count} items",
                    managerResult?.Items?.Count() ?? 0);

                var aggregated = ToDoListResponseAggregator.Merge(
                    managerResult.Items ?? Enumerable.Empty<TaskManagerItemResponseDto>(),
                    stateResult.Items ?? Enumerable.Empty<TaskStateServiceItemResponseDto>()
                );

                var response = new GetToDoListByStatusResponseDto()
                {
                    Items = aggregated.Select(x => new ToDoItemDto
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Details = x.Details,
                        DueDate = x.DueDate,
                        Status = x.Status,
                        Priority = x.Priority
                    })
                };

                _logger.Information(
                    "GetByStatus completed successfully. ResultCount={Count}",
                    response.Items?.Count() ?? 0);

                return new ServiceResult<GetToDoListByStatusResponseDto>
                {
                    Data = response
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "GetByStatus orchestration failed. UserId={UserId}", query.UserId);

                return ServiceResult<GetToDoListByStatusResponseDto>
                    .Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}