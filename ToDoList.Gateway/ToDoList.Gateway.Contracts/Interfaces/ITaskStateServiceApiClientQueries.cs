using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Contracts.ApiClients;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Get;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Get.ServiceQueries;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Get.ResponseContainers;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.ServiceQueries;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Get;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Get.ServiceQueries;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Get.ResponseContainers;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.ServiceQueries;

namespace ToDoList.Gateway.Contracts.Interfaces
{
    public interface ITaskStateServiceApiClientQueries
    {
        Task<ServiceApiResponse<TaskStateServiceGetToDoListByPriorityResponseDto>> GetToDoListAsync(
            TaskStateServiceGetToDoListRequestDto dto);

        Task<ServiceApiResponse<TaskStateServiceGetToDoListByPriorityResponseDto>> GetToDoListByPriorityAsync(
            GetToDoListByPriorityRequestDto dto);

        Task<ServiceApiResponse<TaskStateServiceGetToDoListByStatusResponseDto>> GetToDoListByStatusAsync(
            TaskStateServiceGetToDoListByStatusRequestDto dto);

        Task<ServiceApiResponse<TaskStateServiceGetToDoListByOverdueResponseDto>> GetToDoListByOverdueAsync(
            TaskStateServiceGetToDoListByOverdueRequestDto dto);

        Task<ServiceApiResponse<TaskStateServiceGetToDoListByIdsResponseDto>> GetToDoListByIdAsync(
            TaskStateServiceGetToDoListByIdsRequestDto dto);
    }
}
