using AutoMapper;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.Handlers.ChangeToDoContent;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.Handlers.CreateToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.Handlers.DeleteToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.Handlers.GetListToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.ServiceQueries.GetByIds;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.RabbitEndpoints;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Delete;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Get;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerRequestDtos.RequestDtos.Get.ServiceQueries;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Get.ResponseContainers;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.ServiceQueries;
using ToDoList.Gateway.Contracts.Interfaces;
using ToDoList.Gateway.Infrastructure.Persistance.Rabbit;



namespace ToDoList.Gateway.Infrastructure.Persistance.Adapters
{
    public class TaskManagerApiClientAdapter : ITaskManagerApiClientAdapter
    {
        private readonly ITaskManagerApiClientQueries _clientQuery;
        private readonly IMapper _mapper;
        private readonly RabbitPublisher _publisher;
        public TaskManagerApiClientAdapter(
            ITaskManagerApiClientCommands clientCommand, 
            ITaskManagerApiClientQueries clientQuery, 
            IMapper mapper, 
            RabbitPublisher publisher)
        {
            _clientQuery = clientQuery;
            _mapper = mapper;
            _publisher = publisher;
        }
        public async Task ChangeContentAsync(
            ChangeToDoContentCommand command, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskManagerChangeContentRequestDto>(command);

            await _publisher.PublishAsync(
                TaskManagerEndpoints.Change,
                dto,
                cancellationToken);
        }

        public async Task CreateAsync(
            CreateToDoCommand command,
            Guid id,
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskManagerCreateRequestDto>(command);

            dto.Id = id;

            await _publisher.PublishAsync(
                TaskManagerEndpoints.Create,
                dto,
                cancellationToken);

        }

        public async Task DeleteAsync(
            DeleteToDoCommand command, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskManagerDeleteRequestDto>(command);

            await _publisher.PublishAsync(
                TaskManagerEndpoints.Delete,
                dto,
                cancellationToken);

        }

        public async Task<TaskManagerGetToDoListResponseDto> GetToDoListAsync(
            GetToDoListQuery query, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskManagerGetToDoListRequestDto>(query);

            var response = await _clientQuery.GetToDoListAsync(dto);

            var result = _mapper.Map<TaskManagerGetToDoListResponseDto>(response);

            return result;
        }

        public async Task<TaskManagerGetToDoListByIdsResponseDto> GetToDoListByIdAsync(
            GetToDoListByIdsRequestQuery query, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskManagerGetToDoListByIdsRequestDto>(query);

            var result = await _clientQuery.GetToDoListByIdAsync(dto);

            var mappedDto = _mapper.Map<TaskManagerGetToDoListByIdsResponseDto>(result);

            return mappedDto;
        }
    }
}
