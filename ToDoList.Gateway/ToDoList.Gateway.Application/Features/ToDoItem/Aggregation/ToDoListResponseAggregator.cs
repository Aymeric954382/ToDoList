using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Get;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Get;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Aggregation
{
    public static class ToDoListResponseAggregator
    {
        public static IEnumerable<AggregatedTaskDto> Merge(
            IEnumerable<TaskManagerItemResponseDto> manager,
            IEnumerable<TaskStateServiceItemResponseDto> state)
        {
            var stateMap = state.ToDictionary(s => s.Id);

            foreach (var m in manager)
            {
                if (stateMap.TryGetValue(m.Id, out var s))
                {
                    yield return new AggregatedTaskDto
                    {
                        Id = m.Id,
                        UserId = s.UserId,
                        Title = m.Title,
                        Details = m.Details,
                        Status = s.Status,
                        Priority = s.Priority,
                        DueDate = s.DueDate
                    };
                }
            }
        }   
    }
}
