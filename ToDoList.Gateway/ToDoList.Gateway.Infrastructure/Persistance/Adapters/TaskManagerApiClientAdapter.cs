using AutoMapper;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.ChangeToDoContent;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.CreateToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.DeleteToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetListToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.ServiceQueries;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.ServiceQueries.GetByIds;
using ToDoList.Gateway.Application.Interfaces;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
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
using ToDoList.Gateway.Contracts.Interfaces;



namespace ToDoList.Gateway.Infrastructure.Persistance.Adapters
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
        public async Task<TaskManagerChangeContentResponseDto> ChangeContentAsync(ChangeToDoContentCommand command, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskManagerChangeContentRequestDto>(command);

            var response = await _clientCommand.ChangeContentAsync(dto);

            var result = _mapper.Map<TaskManagerChangeContentResponseDto>(response);

            return result;
        }

        public async Task<TaskManagerCreateResponseDto> CreateAsync(CreateToDoCommand command, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskManagerCreateRequestDto>(command);

            var response = await _clientCommand.CreateAsync(dto);

            var result = _mapper.Map<TaskManagerCreateResponseDto>(response);

            return result;
        }

        public async Task<TaskManagerDeleteResponseDto> DeleteAsync(DeleteToDoCommand command, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskManagerDeleteRequestDto>(command);

            var response = await _clientCommand.DeleteAsync(dto);

            var result = _mapper.Map<TaskManagerDeleteResponseDto>(response);

            return result;
        }

        public async Task<TaskManagerGetToDoListResponseDto> GetToDoListAsync(GetToDoListQuery query, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskManagerGetToDoListRequestDto>(query);

            var response = await _clientQuery.GetToDoListAsync(dto);

            var result = _mapper.Map<TaskManagerGetToDoListResponseDto>(response);

            return result;
        }

        public async Task<TaskManagerGetToDoListByIdsResponseDto> GetToDoListByIdAsync(GetToDoListByIdsRequestQuery query, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskManagerGetToDoListByIdsRequestDto>(query);

            var result = await _clientQuery.GetToDoListByIdAsync(dto);

            var mappedDto = _mapper.Map<TaskManagerGetToDoListByIdsResponseDto>(result);

            return mappedDto;
        }
    }
}
