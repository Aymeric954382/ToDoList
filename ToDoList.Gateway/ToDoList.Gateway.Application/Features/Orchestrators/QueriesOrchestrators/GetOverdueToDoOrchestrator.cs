using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Features.ToDoItem.Aggregation;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.DomainResponseDtos;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetByStatus;
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

        public GetOverdueToDoOrchestrator(ITaskManagerApiClientAdapter taskManagerApiClient, 
            ITaskStateServiceApiClientAdapter taskStateServiceApiClient)
        {
            _taskManagerApiClient = taskManagerApiClient;
            _taskStateServiceApiClient = taskStateServiceApiClient;
        }

        public async Task<ServiceResult<GetToDoListByOverdueResponseDto>> GetListByOverdueAsync(
            GetToDoListByOverdueQuery query,
            CancellationToken cancellationToken)
        {
            var stateResult = await _taskStateServiceApiClient.GetToDoListByOverdueAsync(query, cancellationToken);

            if (!stateResult.ExecutionSuccess)
                return ServiceResult<GetToDoListByOverdueResponseDto>.Fail(
                    stateResult.Error ?? ServiceErrorCode.Unknown);
            try
            {
                var ids = stateResult.Data.Items.Select(x => x.Id);

                var getByIdQuery = new GetToDoListByIdsRequestQuery()
                {
                    Ids = ids,
                    UserId = query.UserId
                };

                var managerResult = await _taskManagerApiClient.GetToDoListByIdAsync(getByIdQuery, cancellationToken);

                var aggregated = ToDoListResponseAggregator.Merge(
                    managerResult.Data.Items ?? Enumerable.Empty<TaskManagerItemResponseDto>(),
                    stateResult.Data.Items ?? Enumerable.Empty<TaskStateServiceItemResponseDto>()
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

                return new ServiceResult<GetToDoListByOverdueResponseDto>() { Data = response };
            }
            catch (HttpRequestException)
            {
                return ServiceResult<GetToDoListByOverdueResponseDto>.Fail(
                    ServiceErrorCode.ServiceUnavailable);
            }
            catch (Exception ex)
            {
                //logger

                return ServiceResult<GetToDoListByOverdueResponseDto>.Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}
