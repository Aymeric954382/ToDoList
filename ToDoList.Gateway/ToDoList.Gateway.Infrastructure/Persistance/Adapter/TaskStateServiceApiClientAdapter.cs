


using AutoMapper;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using ToDoList.Gateway.Application.ToDoItem.Commands.ChangeToDoDueDate;
using ToDoList.Gateway.Application.ToDoItem.Commands.ChangeToDoPriority;
using ToDoList.Gateway.Application.ToDoItem.Commands.ChangeToDoStatus;
using ToDoList.Gateway.Application.ToDoItem.Commands.CreateToDo;
using ToDoList.Gateway.Application.ToDoItem.Commands.DeleteToDo;
using ToDoList.Gateway.Application.ToDoItem.Queries.Containers;
using ToDoList.Gateway.Application.ToDoItem.Queries.CriteriaSplitter;
using ToDoList.Gateway.Application.ToDoItem.Queries.GetByPriority;
using ToDoList.Gateway.Application.ToDoItem.Queries.GetByStatus;
using ToDoList.Gateway.Application.ToDoItem.Queries.GetListToDo;
using ToDoList.Gateway.Application.ToDoItem.Queries.GetOverdueToDo;
using ToDoList.Gateway.Contracts.ApiClients.Interfaces;
using ToDoList.Gateway.Contracts.ApiClients.RequestDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.RequestDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.RequestDtos.Get;
using ToDoList.Gateway.Contracts.ApiClients.ResponseDtos;

namespace ToDoList.Gateway.Infrastructure.Persistance.Adapter
{
    public class TaskStateServiceApiClientAdapter : ITaskStateServiceApiClientAdapter
    {
        private readonly ITaskStateServiceApiClientCommands _clientCommand;
        private readonly ITaskStateClientApiClientQueries _clientQuery;
        private readonly IMapper _mapper;
        public TaskStateServiceApiClientAdapter(ITaskStateServiceApiClientCommands clientCommand, ITaskStateClientApiClientQueries clientQuery, IMapper mapper)
        {
            _clientCommand = clientCommand;
            _clientQuery = clientQuery;
            _mapper = mapper;
        }

        public async Task<HttpResponseMessage> ChangeDueDateAsync(ChangeToDoDueDateCommand command)
        {
            var dto = _mapper.Map<ChangeToDoDueDateDto>(command);

            var result = await _clientCommand.ChangeDueDateAsync(dto);

            return result;
        }

        public async Task<HttpResponseMessage> ChangeStatusAsync(ChangeToDoStatusCommand command)
        {
            var dto = _mapper.Map<ChangeToDoStatusDto>(command);

            var result = await _clientCommand.ChangeStatusAsync(dto);

            return result;
        }

        public async Task<HttpResponseMessage> ChangePriorityAsync(ChangeToDoPriorityCommand command)
        {
            var dto = _mapper.Map<ChangeToDoPriorityDto>(command);

            var result = await _clientCommand.ChangePriorityAsync(dto);

            return result;
        }

        public async Task<HttpResponseMessage> CreateAsync(CreateToDoCommand command, Guid id)
        {
            var dto = _mapper.Map<CreateForServiceToDoDto>(command);

            dto.Id = id;

            var result = await _clientCommand.CreateAsync(dto);

            return result;
        }

        public async Task<HttpResponseMessage> DeleteAsync(DeleteToDoCommand command)
        {
            var dto = _mapper.Map<GetToDoListOverdueDto>(command);

            var result = await _clientCommand.DeleteAsync(dto);

            return result;
        }

        public async Task<ToDoListContainer> GetToDoListByPriorityAsync(CriteriaSplitterQuery query)
        {
            var dto = _mapper.Map<GetToDoListByPriorityDto>(query);

            var result = await _clientQuery.GetToDoListByPriorityAsync(dto);

            var container = _mapper.Map<ToDoListContainer>(result);

            return container;
        }

        public async Task<ToDoListContainer> GetToDoListByStatusAsync(CriteriaSplitterQuery query)
        {
            var dto = _mapper.Map<GetToDoListByStatusDto>(query);

            var result = await _clientQuery.GetToDoListByStatusAsync(dto);

            var container = _mapper.Map<ToDoListContainer>(result);

            return container;
        }

        public async Task<ToDoListContainer> GetToDoListByOverdueAsync(CriteriaSplitterQuery query)
        {
            var dto = _mapper.Map<GetToDoListByOverdueDto>(query);

            var result = await _clientQuery.GetToDoListByOverdueAsync(dto);

            var container = _mapper.Map<ToDoListContainer>(result);

            return container;
        }

        public async Task<ToDoListContainer> GetToDoListAsync(CriteriaSplitterQuery query)
        {
            var dto = _mapper.Map<GetToDoListDto>(query);

            var result = await _clientQuery.GetToDoListAsync(dto);

            var container = _mapper.Map<ToDoListContainer>(result);

            return container;
        }
    }
}
