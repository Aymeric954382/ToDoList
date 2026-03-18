using AutoMapper;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.ChangeToDoContent;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.CreateToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.DeleteToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetListToDo;
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



namespace ToDoList.Gateway.Infrastructure.Persistance.Adapter
{
    public class TaskManagerApiClientAdapter : ITaskManagerApiClientAdapter
    {
        private readonly ITaskManagerApiClientCommands _clientCommand;
        private readonly ITaskManagerApiClientQueries _clientQuery;
        private readonly IMapper _mapper;
        public TaskManagerApiClientAdapter(ITaskManagerApiClientCommands clientCommand, 
            ITaskManagerApiClientQueries clientQuery, IMapper mapper)
        {
            _clientCommand = clientCommand;
            _clientQuery = clientQuery;
            _mapper = mapper;
        }
        public async Task<ServiceResult<TaskManagerChangeContentResponseDto>> ChangeContentAsync(ChangeToDoContentCommand command, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskManagerChangeContentRequestDto>(command);

            var response = await _clientCommand.ChangeContentAsync(dto);

            var result = _mapper.Map<ServiceResult<TaskManagerChangeContentResponseDto>>(response);

            return result;
        }

        public async Task<ServiceResult<TaskManagerCreateResponseDto>> CreateAsync(CreateToDoCommand command, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskManagerCreateRequestDto>(command);

            var response = await _clientCommand.CreateAsync(dto);

            var result = _mapper.Map<ServiceResult<TaskManagerCreateResponseDto>>(response);

            return result;
        }

        public async Task<ServiceResult<TaskManagerDeleteResponseDto>> DeleteAsync(DeleteToDoCommand command, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskManagerDeleteRequestDto>(command);

            var response = await _clientCommand.DeleteAsync(dto);

            var result = _mapper.Map<ServiceResult<TaskManagerDeleteResponseDto>>(response);

            return result;
        }

        public async Task<ServiceResult<TaskManagerGetToDoListResponseDto>> GetToDoListAsync(GetToDoListQuery query, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskManagerGetToDoListRequestDto>(query);

            var response = await _clientQuery.GetToDoListAsync(dto);

            var result = _mapper.Map<ServiceResult<TaskManagerGetToDoListResponseDto>>(response);

            return result;
        }

        public async Task<ServiceResult<TaskManagerGetToDoListByIdsResponseDto>> GetToDoListByIdAsync(GetToDoListByIdsRequestQuery query, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskManagerGetToDoListByIdsRequestDto>(query);

            var result = await _clientQuery.GetToDoListByIdAsync(dto);

            var container = _mapper.Map<ServiceResult<TaskManagerGetToDoListByIdsResponseDto>>(result);

            return container;
        }
    }
}
