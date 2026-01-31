using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using ToDoList.Gateway.Application.ToDoItem.Commands.ChangeToDoContent;
using ToDoList.Gateway.Application.ToDoItem.Commands.CreateToDo;
using ToDoList.Gateway.Application.ToDoItem.Commands.DeleteToDo;
using ToDoList.Gateway.Application.ToDoItem.Queries.Containers;
using ToDoList.Gateway.Application.ToDoItem.Queries.CriteriaSplitter;
using ToDoList.Gateway.Application.ToDoItem.Queries.GetListToDo;
using ToDoList.Gateway.Contracts.ApiClients.Interfaces;
using ToDoList.Gateway.Contracts.ApiClients.RequestDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.RequestDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.RequestDtos.Get;
using ToDoList.Gateway.Contracts.ApiClients.ResponseDtos;

namespace ToDoList.Gateway.Infrastructure.Persistance.Adapter
{
    public class TaskManagerApiClientAdapter : ITaskManagerApiClientAdapter
    {
        private readonly ITaskManagerApiClientCommands _clientCommand;
        private readonly ITaskStateClientApiClientQueries _clientQuery;
        private readonly IMapper _mapper;
        public TaskManagerApiClientAdapter(ITaskManagerApiClientCommands clientCommand, ITaskStateClientApiClientQueries clientQuery, IMapper mapper)
        {
            _clientCommand = clientCommand;
            _clientQuery = clientQuery;
            _mapper = mapper;
        }
        public async Task<HttpResponseMessage> ChangeContentAsync(ChangeToDoContentCommand command)
        {
            var dto = _mapper.Map<ChangeToDoContentDto>(command);

            var result = await _clientCommand.ChangeContentAsync(dto);

            return result;
        }

        public async Task<Guid> CreateAsync(CreateToDoCommand command)
        {
            var dto = _mapper.Map<CreateForManagerToDoDto>(command);

            var result = await _clientCommand.CreateAsync(dto);

            return result.Id;
        }

        public async Task<HttpResponseMessage> DeleteAsync(DeleteToDoCommand command)
        {
            var dto = _mapper.Map<GetToDoListOverdueDto>(command);

            var result = await _clientCommand.DeleteAsync(dto);

            return result;
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
