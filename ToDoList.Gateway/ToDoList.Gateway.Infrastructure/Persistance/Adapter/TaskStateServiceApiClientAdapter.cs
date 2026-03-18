


using AutoMapper;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.ChangeToDoDueDate;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.ChangeToDoPriority;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.ChangeToDoStatus;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.CreateToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.DeleteToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetByPriority;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetByStatus;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetListToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetOverdueToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.ServiceQueries;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.ServiceQueries.GetByIds;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using ToDoList.Gateway.Contracts.ApiClients.Interfaces;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Delete;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Get;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Get.ServiceQueries;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Delete;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Get.ResponseContainers;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.ServiceQueries;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Delete;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Get;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Get.ServiceQueries;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Delete;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Get.ResponseContainers;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.ServiceQueries;

namespace ToDoList.Gateway.Infrastructure.Persistance.Adapter
{
    public class TaskStateServiceApiClientAdapter : ITaskStateServiceApiClientAdapter
    {
        private readonly ITaskStateServiceApiClientCommands _clientCommand;
        private readonly ITaskStateServiceApiClientQueries _clientQuery;
        private readonly IMapper _mapper;
        public TaskStateServiceApiClientAdapter(ITaskStateServiceApiClientCommands clientCommand, 
            ITaskStateServiceApiClientQueries clientQuery, IMapper mapper)
        {
            _clientCommand = clientCommand;
            _clientQuery = clientQuery;
            _mapper = mapper;
        }

        public async Task<ServiceResult<TaskStateServiceChangeDueDateResponseDto>> ChangeDueDateAsync(ChangeToDoDueDateCommand command, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskStateServiceChangeDueDateRequestDto>(command);

            var response = await _clientCommand.ChangeDueDateAsync(dto);

            var result = _mapper.Map<ServiceResult<TaskStateServiceChangeDueDateResponseDto>>(response);

            return result;
        }

        public async Task<ServiceResult<TaskStateServiceChangeStatusResponseDto>> ChangeStatusAsync(ChangeToDoStatusCommand command, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskStateServiceChangeStatusRequestDto>(command);

            var response = await _clientCommand.ChangeStatusAsync(dto);

            var result = _mapper.Map<ServiceResult<TaskStateServiceChangeStatusResponseDto>>(response);

            return result;
        }

        public async Task<ServiceResult<TaskStateServiceChangePriorityResponseDto>> ChangePriorityAsync(ChangeToDoPriorityCommand command, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskStateServiceChangePriorityRequestDto>(command);

            var response = await _clientCommand.ChangePriorityAsync(dto);

            var result = _mapper.Map<ServiceResult<TaskStateServiceChangePriorityResponseDto>>(response);

            return result;
        }

        public async Task<ServiceResult<TaskStateServiceCreateResponseDto>> CreateAsync(CreateToDoCommand command, Guid id, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskStateServiceCreateRequestDto>(command);

            dto.Id = id;

            var response = await _clientCommand.CreateAsync(dto);

            var result = _mapper.Map<ServiceResult<TaskStateServiceCreateResponseDto>>(response);

            return result;
        }

        public async Task<ServiceResult<TaskStateServiceDeleteResponseDto>> DeleteAsync(DeleteToDoCommand command, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskStateServiceDeleteRequestDto>(command);

            var response = await _clientCommand.DeleteAsync(dto);

            var result = _mapper.Map<ServiceResult<TaskStateServiceDeleteResponseDto>>(response);

            return result;
        }

        public async Task<ServiceResult<TaskStateServiceGetToDoListByPriorityResponseDto>> GetToDoListByPriorityAsync(GetToDoListByPriorityQuery query, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<GetToDoListByPriorityRequestDto>(query);

            var result = await _clientQuery.GetToDoListByPriorityAsync(dto);

            var container = _mapper.Map<ServiceResult<TaskStateServiceGetToDoListByPriorityResponseDto>>(result);

            return container;
        }

        public async Task<ServiceResult<TaskStateServiceGetToDoListByStatusResponseDto>> GetToDoListByStatusAsync(GetToDoListByStatusQuery query, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskStateServiceGetToDoListByStatusRequestDto>(query);

            var result = await _clientQuery.GetToDoListByStatusAsync(dto);

            var container = _mapper.Map<ServiceResult<TaskStateServiceGetToDoListByStatusResponseDto>>(result);

            return container;
        }

        public async Task<ServiceResult<TaskStateServiceGetToDoListByOverdueResponseDto>> GetToDoListByOverdueAsync(GetToDoListByOverdueQuery query, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskStateServiceGetToDoListByOverdueRequestDto>(query);

            var result = await _clientQuery.GetToDoListByOverdueAsync(dto);

            var container = _mapper.Map<ServiceResult<TaskStateServiceGetToDoListByOverdueResponseDto>>(result);

            return container;
        }

        public async Task<ServiceResult<TaskStateServiceGetToDoListByPriorityResponseDto>> GetToDoListAsync(GetToDoListQuery query, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskStateServiceGetToDoListRequestDto>(query);

            var result = await _clientQuery.GetToDoListAsync(dto);

            var container = _mapper.Map<ServiceResult<TaskStateServiceGetToDoListByPriorityResponseDto>>(result);

            return container;
        }

        public async Task<ServiceResult<TaskStateServiceGetToDoListByIdsResponseDto>> GetToDoListAsync(GetToDoListByIdsRequestQuery query, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskStateServiceGetToDoListByIdsRequestDto>(query);

            var result = await _clientQuery.GetToDoListByIdAsync(dto);

            var container = _mapper.Map<ServiceResult<TaskStateServiceGetToDoListByIdsResponseDto>>(result);

            return container;
        }
    }
}
