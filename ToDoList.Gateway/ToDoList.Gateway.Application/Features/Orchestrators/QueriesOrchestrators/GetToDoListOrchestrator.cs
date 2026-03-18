using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Features.ToDoItem.Aggregation;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.DomainResponseDtos;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetListToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetOverdueToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.ServiceQueries.GetByIds;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using ToDoList.Gateway.Application.Interfaces.Orchestartors;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Get;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Get;

namespace ToDoList.Gateway.Application.Features.Orchestrators.QueriesOrchestrators
{
    public class GetToDoListOrchestrator : IGetToDoListOrchestrator
    {
        private readonly ITaskManagerApiClientAdapter _taskManagerApiClient;
        private readonly ITaskStateServiceApiClientAdapter _taskStateServiceApiClient;

        public GetToDoListOrchestrator(
            ITaskManagerApiClientAdapter taskManagerApiClient,
            ITaskStateServiceApiClientAdapter taskStateServiceApiClient)
        {
            _taskManagerApiClient = taskManagerApiClient;
            _taskStateServiceApiClient = taskStateServiceApiClient;
        }
        public async Task<ServiceResult<GetToDoListResponseDto>> GetListAsync(
            GetToDoListQuery query, 
            CancellationToken cancellationToken)
        {
            var stateResult = await _taskStateServiceApiClient.GetToDoListAsync(query, cancellationToken);

            if (!stateResult.ExecutionSuccess)
                return ServiceResult<GetToDoListResponseDto>.Fail(
                    stateResult.Error ?? ServiceErrorCode.Unknown);
            try
            {
                var managerResult = await _taskManagerApiClient.GetToDoListAsync(query, cancellationToken);

                var aggregated = ToDoListResponseAggregator.Merge(
                    managerResult.Data.Items ?? Enumerable.Empty<TaskManagerItemResponseDto>(),
                    stateResult.Data.Items ?? Enumerable.Empty<TaskStateServiceItemResponseDto>()
                );

                var response = new GetToDoListResponseDto()
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

                return new ServiceResult<GetToDoListResponseDto>() { Data = response };
            }
            catch (HttpRequestException)
            {
                return ServiceResult<GetToDoListResponseDto>.Fail(
                    ServiceErrorCode.ServiceUnavailable);
            }
            catch (Exception ex)
            {
                //logger

                return ServiceResult<GetToDoListResponseDto>.Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}
