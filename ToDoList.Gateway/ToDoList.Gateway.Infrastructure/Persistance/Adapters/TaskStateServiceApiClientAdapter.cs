


using AutoMapper;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.Handlers.ChangeToDoDueDate;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.Handlers.ChangeToDoPriority;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.Handlers.ChangeToDoStatus;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.Handlers.CreateToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.Handlers.DeleteToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.Handlers.GetByPriority;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.Handlers.GetByStatus;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.Handlers.GetListToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.Handlers.GetOverdueToDo;
using ToDoList.Gateway.Application.Features.ToDoItem.Queries.ServiceQueries.GetByIds;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.RabbitEndpoints;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Delete;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Get;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceRequestDtos.RequestDtos.Get.ServiceQueries;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Get.ResponseContainers;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.ServiceQueries;
using ToDoList.Gateway.Contracts.Interfaces;
using ToDoList.Gateway.Infrastructure.Persistance.Rabbit;

namespace ToDoList.Gateway.Infrastructure.Persistance.Adapters
{
    public class TaskStateServiceApiClientAdapter : ITaskStateServiceApiClientAdapter
    {
        private readonly ITaskStateServiceApiClientQueries _clientQuery;
        private readonly IMapper _mapper;
        private readonly RabbitPublisher _rabbitPublisher;
        public TaskStateServiceApiClientAdapter(
            ITaskStateServiceApiClientCommands clientCommand, 
            ITaskStateServiceApiClientQueries clientQuery, 
            IMapper mapper,
            RabbitPublisher rabbitPublisher)

        {
            _clientQuery = clientQuery;
            _mapper = mapper;
            _rabbitPublisher = rabbitPublisher;
        }

        public async Task ChangeDueDateAsync(
            ChangeToDoDueDateCommand command, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskStateServiceChangeDueDateRequestDto>(command);

            await _rabbitPublisher
                .PublishAsync(
                TaskStateServiceEndpoints.ChangeDueDate,
                dto,
                cancellationToken);
        }

        public async Task ChangeStatusAsync(
            ChangeToDoStatusCommand command, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskStateServiceChangeStatusRequestDto>(command);

            await _rabbitPublisher
                .PublishAsync(
                TaskStateServiceEndpoints.ChangeStatus,
                dto,
                cancellationToken);
        }

        public async Task ChangePriorityAsync(
            ChangeToDoPriorityCommand command, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskStateServiceChangePriorityRequestDto>(command);

            await _rabbitPublisher
                .PublishAsync(
                TaskStateServiceEndpoints.ChangePriority,
                dto,
                cancellationToken);
        }

        public async Task CreateAsync(
            CreateToDoCommand command, 
            Guid id, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskStateServiceCreateRequestDto>(command);

            dto.Id = id;

            await _rabbitPublisher
                .PublishAsync(
                TaskStateServiceEndpoints.Create,
                dto,
                cancellationToken);

        }

        public async Task DeleteAsync(
            DeleteToDoCommand command, 
            CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<TaskStateServiceDeleteRequestDto>(command);

            await _rabbitPublisher
                .PublishAsync(
                TaskStateServiceEndpoints.Delete,
                dto,
                cancellationToken);
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
