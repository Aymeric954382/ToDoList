using ToDoList.Gateway.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Features.ToDoItem.Aggregation;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.DomainResponseDtos;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetByPriority;
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

        public GetByStatusOrchestrator(
            ITaskManagerApiClientAdapter taskManagerApiClientAdapter, 
            ITaskStateServiceApiClientAdapter taskStateServiceApiClientAdapter)
        {
            _taskManagerApiClientAdapter = taskManagerApiClientAdapter;
            _taskStateServiceApiClientAdapter = taskStateServiceApiClientAdapter;
        }
        public async Task<ServiceResult<GetToDoListByStatusResponseDto>> GetListByStatusAsync(
            GetToDoListByStatusQuery query,
            CancellationToken cancellationToken)
        {
            var stateResult = await _taskStateServiceApiClientAdapter.GetToDoListByStatusAsync(query, cancellationToken);

            if (!stateResult.ExecutionSuccess)
                return ServiceResult<GetToDoListByStatusResponseDto>.Fail(
                    stateResult.Error ?? ServiceErrorCode.Unknown);
            try
            {
                var ids = stateResult.Data.Items.Select(x => x.Id);

                var getByIdQuery = new GetToDoListByIdsRequestQuery()
                {
                    Ids = ids,
                    UserId = query.UserId
                };

                var managerResult = await _taskManagerApiClientAdapter.GetToDoListByIdAsync(getByIdQuery, cancellationToken);

                if (!managerResult.ExecutionSuccess)
                    return ServiceResult<GetToDoListByStatusResponseDto>.Fail(
                        managerResult.Error ?? ServiceErrorCode.Unknown);

                var aggregated = ToDoListResponseAggregator.Merge(
                    managerResult.Data.Items ?? Enumerable.Empty<TaskManagerItemResponseDto>(),
                    stateResult.Data.Items ?? Enumerable.Empty<TaskStateServiceItemResponseDto>()
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

                return new ServiceResult<GetToDoListByStatusResponseDto>() { Data = response };
            }
            catch(HttpRequestException)
            {
                return ServiceResult<GetToDoListByStatusResponseDto>.Fail(
                    ServiceErrorCode.ServiceUnavailable);
            }
            catch (Exception ex)
            {
                //logger

                return ServiceResult<GetToDoListByStatusResponseDto>.Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}
 