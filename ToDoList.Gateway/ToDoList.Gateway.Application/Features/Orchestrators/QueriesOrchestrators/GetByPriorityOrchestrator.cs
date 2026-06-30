using Serilog;
using ToDoList.Gateway.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Features.ToDoItem.Aggregation;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.DomainResponseDtos;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetByPriority;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.ServiceQueries.GetByIds;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Get.ServiceQueries;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Get;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Get;

namespace ToDoList.Gateway.Application.Features.Orchestrators.QueriesOrchestrators
{
    public class GetByPriorityOrchestrator
    {
        private readonly ITaskStateServiceApiClientAdapter _clientStateServiceAdapter;
        private readonly ITaskManagerApiClientAdapter _clientManagerAdapter;
        private readonly ILogger _logger;

        public GetByPriorityOrchestrator(
            ITaskStateServiceApiClientAdapter clientStateServiceAdapter,
            ITaskManagerApiClientAdapter clientManagerAdapter,
            ILogger logger)
        {
            _clientStateServiceAdapter = clientStateServiceAdapter;
            _clientManagerAdapter = clientManagerAdapter;
            _logger = logger;
        }

        public async Task<ServiceResult<GetToDoListByPriorityResponseDto>> GetByPriorityAsync(
            GetToDoListByPriorityQuery query,
            CancellationToken cancellationToken)
        {
            _logger.Information("GetByPriority orchestration started. UserId={UserId}", query.UserId);

            try
            {
                var stateResult = await _clientStateServiceAdapter
                    .GetToDoListByPriorityAsync(query, cancellationToken);

                _logger.Information(
                    "StateService returned {Count} items",
                    stateResult?.Items?.Count() ?? 0);

                var ids = stateResult.Items.Select(x => x.Id).ToList();

                var getByIdQuery = new GetToDoListByIdsRequestQuery()
                {
                    Ids = ids,
                    UserId = query.UserId
                };

                var managerResult = await _clientManagerAdapter
                    .GetToDoListByIdAsync(getByIdQuery, cancellationToken);

                _logger.Information(
                    "ManagerService returned {Count} items",
                    managerResult?.Items?.Count() ?? 0);

                var aggregated = ToDoListResponseAggregator.Merge(
                    managerResult.Items ?? Enumerable.Empty<TaskManagerItemResponseDto>(),
                    stateResult.Items ?? Enumerable.Empty<TaskStateServiceItemResponseDto>()
                );

                var response = new GetToDoListByPriorityResponseDto
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
                    "GetByPriority completed successfully. ResultCount={Count}",
                    response.Items?.Count() ?? 0);

                return new ServiceResult<GetToDoListByPriorityResponseDto>
                {
                    Data = response
                };
            }
            catch (HttpRequestException ex)
            {
                _logger.Warning(ex, "External service unavailable in GetByPriority");

                return ServiceResult<GetToDoListByPriorityResponseDto>.Fail(
                    ServiceErrorCode.ServiceUnavailable);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Unexpected error in GetByPriority orchestration");

                return ServiceResult<GetToDoListByPriorityResponseDto>
                    .Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}