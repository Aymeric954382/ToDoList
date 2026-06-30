using Serilog;
using ToDoList.Gateway.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Features.ToDoItem.Aggregation;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.DomainResponseDtos;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetOverdueToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.ServiceQueries.GetByIds;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using ToDoList.Gateway.Application.Interfaces.Orchestartors;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Get;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Get;

namespace ToDoList.Gateway.Application.Features.Orchestrators.QueriesOrchestrators
{
    public class GetOverdueToDoOrchestrator : IGetToDoListByOverdueOrchestrator
    {
        private readonly ITaskManagerApiClientAdapter _taskManagerApiClient;
        private readonly ITaskStateServiceApiClientAdapter _taskStateServiceApiClient;
        private readonly ILogger _logger;

        public GetOverdueToDoOrchestrator(
            ITaskManagerApiClientAdapter taskManagerApiClient,
            ITaskStateServiceApiClientAdapter taskStateServiceApiClient,
            ILogger logger)
        {
            _taskManagerApiClient = taskManagerApiClient;
            _taskStateServiceApiClient = taskStateServiceApiClient;
            _logger = logger;
        }

        public async Task<ServiceResult<GetToDoListByOverdueResponseDto>> GetListByOverdueAsync(
            GetToDoListByOverdueQuery query,
            CancellationToken cancellationToken)
        {
            _logger.Information("GetOverdue orchestration started. UserId={UserId}", query.UserId);

            try
            {
                var stateResult = await _taskStateServiceApiClient
                    .GetToDoListByOverdueAsync(query, cancellationToken);

                _logger.Information(
                    "StateService returned {Count} items",
                    stateResult?.Items?.Count() ?? 0);

                var ids = stateResult.Items?.Select(x => x.Id)
                          ?? Enumerable.Empty<Guid>();

                var getByIdQuery = new GetToDoListByIdsRequestQuery()
                {
                    Ids = ids,
                    UserId = query.UserId
                };

                var managerResult = await _taskManagerApiClient
                    .GetToDoListByIdAsync(getByIdQuery, cancellationToken);

                _logger.Information(
                    "ManagerService returned {Count} items",
                    managerResult?.Items?.Count() ?? 0);

                var aggregated = ToDoListResponseAggregator.Merge(
                    managerResult.Items ?? Enumerable.Empty<TaskManagerItemResponseDto>(),
                    stateResult.Items ?? Enumerable.Empty<TaskStateServiceItemResponseDto>()
                );

                var response = new GetToDoListByOverdueResponseDto()
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
                    "GetOverdue completed successfully. ResultCount={Count}",
                    response.Items?.Count() ?? 0);

                return new ServiceResult<GetToDoListByOverdueResponseDto>
                {
                    Data = response
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "GetOverdue orchestration failed. UserId={UserId}", query.UserId);

                return ServiceResult<GetToDoListByOverdueResponseDto>
                    .Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}