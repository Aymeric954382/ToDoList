


using AutoMapper;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.ChangeToDoDueDate;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.ChangeToDoPriority;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.ChangeToDoStatus;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.CreateToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.DeleteToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetByPriority;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetByStatus;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetListToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetOverdueToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.ServiceQueries.GetByIds;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
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
using ToDoList.Gateway.Contracts.Interfaces;

namespace ToDoList.Gateway.Infrastructure.Persistance.Adapters
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

        public async Task<TaskStateServiceChangeDueDateResponseDto> ChangeDueDateAsync(
            ChangeToDoDueDateCommand command, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskStateServiceChangeDueDateRequestDto>(command);

            var response = await _clientCommand.ChangeDueDateAsync(dto);

            var result = _mapper.Map<TaskStateServiceChangeDueDateResponseDto>(response);

            return result;
        }

        public async Task<TaskStateServiceChangeStatusResponseDto> ChangeStatusAsync(
            ChangeToDoStatusCommand command, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskStateServiceChangeStatusRequestDto>(command);

            var response = await _clientCommand.ChangeStatusAsync(dto);

            var result = _mapper.Map<TaskStateServiceChangeStatusResponseDto>(response);

            return result;
        }

        public async Task<TaskStateServiceChangePriorityResponseDto> ChangePriorityAsync(
            ChangeToDoPriorityCommand command, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskStateServiceChangePriorityRequestDto>(command);

            var response = await _clientCommand.ChangePriorityAsync(dto);

            var result = _mapper.Map<TaskStateServiceChangePriorityResponseDto>(response);

            return result;
        }

        public async Task<TaskStateServiceCreateResponseDto> CreateAsync(
            CreateToDoCommand command, Guid id, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskStateServiceCreateRequestDto>(command);

            dto.Id = id;

            var response = await _clientCommand.CreateAsync(dto);

            var result = _mapper.Map<TaskStateServiceCreateResponseDto>(response);

            return result;
        }

        public async Task<TaskStateServiceDeleteResponseDto> DeleteAsync(
            DeleteToDoCommand command, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskStateServiceDeleteRequestDto>(command);

            var response = await _clientCommand.DeleteAsync(dto);

            var result = _mapper.Map<TaskStateServiceDeleteResponseDto>(response);

            return result;
        }

        public async Task<TaskStateServiceGetToDoListByPriorityResponseDto> GetToDoListByPriorityAsync(
            GetToDoListByPriorityQuery query, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<GetToDoListByPriorityRequestDto>(query);

            var result = await _clientQuery.GetToDoListByPriorityAsync(dto);

            var mappedDto = _mapper.Map<TaskStateServiceGetToDoListByPriorityResponseDto>(result);

            return mappedDto;
        }

        public async Task<TaskStateServiceGetToDoListByStatusResponseDto> GetToDoListByStatusAsync(
            GetToDoListByStatusQuery query, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskStateServiceGetToDoListByStatusRequestDto>(query);

            var result = await _clientQuery.GetToDoListByStatusAsync(dto);

            var mappedDto = _mapper.Map<TaskStateServiceGetToDoListByStatusResponseDto>(result);

            return mappedDto;
        }

        public async Task<TaskStateServiceGetToDoListByOverdueResponseDto> GetToDoListByOverdueAsync(
            GetToDoListByOverdueQuery query, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskStateServiceGetToDoListByOverdueRequestDto>(query);

            var result = await _clientQuery.GetToDoListByOverdueAsync(dto);

            var mappedDto = _mapper.Map<TaskStateServiceGetToDoListByOverdueResponseDto>(result);

            return mappedDto;
        }

        public async Task<TaskStateServiceGetToDoListByPriorityResponseDto> GetToDoListAsync(
            GetToDoListQuery query, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskStateServiceGetToDoListRequestDto>(query);

            var result = await _clientQuery.GetToDoListAsync(dto);

            var mappedDto = _mapper.Map<TaskStateServiceGetToDoListByPriorityResponseDto>(result);

            return mappedDto;
        }

        public async Task<TaskStateServiceGetToDoListByIdsResponseDto> GetToDoListByIdsAsync(
            GetToDoListByIdsRequestQuery query, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskStateServiceGetToDoListByIdsRequestDto>(query);

            var result = await _clientQuery.GetToDoListByIdAsync(dto);

            var mappedDto = _mapper.Map<TaskStateServiceGetToDoListByIdsResponseDto>(result);

            return mappedDto;
        }
    }
}
