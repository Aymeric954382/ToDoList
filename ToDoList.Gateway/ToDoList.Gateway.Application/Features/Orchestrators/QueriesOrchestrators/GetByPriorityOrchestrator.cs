using ToDoList.Gateway.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.Gateway.Application.Common.Mappings.Helpers;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Features.ToDoItem.Aggregation;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.CreateToDo;
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
        public GetByPriorityOrchestrator(ITaskStateServiceApiClientAdapter clientStateServiceAdapter, 
            ITaskManagerApiClientAdapter clientManagerAdapter)
        {
            _clientStateServiceAdapter = clientStateServiceAdapter;
            _clientManagerAdapter = clientManagerAdapter;
        }
        public async Task<ServiceResult<GetToDoListByPriorityResponseDto>> GetByPriorityAsync(
            GetToDoListByPriorityQuery query, 
            CancellationToken cancellationToken)
        {
            var stateResult = await _clientStateServiceAdapter.GetToDoListByPriorityAsync(query, cancellationToken);

            if (!stateResult.ExecutionSuccess)
                return ServiceResult<GetToDoListByPriorityResponseDto>.Fail(
                    stateResult.Error ?? ServiceErrorCode.Unknown);

            try
            {
                var ids = stateResult.Data.Items.Select(x => x.Id).ToList();

                var getByIdQuery = new GetToDoListByIdsRequestQuery()
                {
                    Ids = ids,
                    UserId = query.UserId
                };

                var managerResult = await _clientManagerAdapter.GetToDoListByIdAsync(getByIdQuery, cancellationToken);

                if (!managerResult.ExecutionSuccess)
                    return ServiceResult<GetToDoListByPriorityResponseDto>.Fail(
                        managerResult.Error ?? ServiceErrorCode.Unknown);

                var aggregated = ToDoListResponseAggregator.Merge(
                    managerResult.Data.Items ?? Enumerable.Empty<TaskManagerItemResponseDto>(),
                    stateResult.Data.Items ?? Enumerable.Empty<TaskStateServiceItemResponseDto>()
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

                return new ServiceResult<GetToDoListByPriorityResponseDto>() { Data = response };

            }
            catch (HttpRequestException)
            {
                return ServiceResult<GetToDoListByPriorityResponseDto>.Fail(
                    ServiceErrorCode.ServiceUnavailable);
            }
            catch (Exception ex)
            {
                //logger

                return ServiceResult<GetToDoListByPriorityResponseDto>.Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}
