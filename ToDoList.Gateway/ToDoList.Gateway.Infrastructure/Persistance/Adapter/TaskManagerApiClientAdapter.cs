using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using ToDoList.Gateway.Application.ToDoItem.Commands.ChangeToDoContent;
using ToDoList.Gateway.Application.ToDoItem.Commands.ChangeToDoStatus;
using ToDoList.Gateway.Application.ToDoItem.Commands.CreateToDo;
using ToDoList.Gateway.Application.ToDoItem.Commands.DeleteToDo;
using ToDoList.Gateway.Contracts.ApiClients.Interfaces;
using ToDoList.Gateway.Contracts.ApiClients.RequestDtos;
using ToDoList.Gateway.Contracts.ApiClients.ResponseDtos;

namespace ToDoList.Gateway.Infrastructure.Persistance.Adapter
{
    public class TaskManagerApiClientAdapter : ITaskManagerApiClientAdapter
    {
        private readonly ITaskManagerApiClient _client;
        private readonly IMapper _mapper;
        public TaskManagerApiClientAdapter(ITaskManagerApiClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }
        public async Task<HttpResponseMessage> ChangeContentAsync(ChangeToDoContentCommand command)
        {
            var dto = _mapper.Map<ChangeToDoContentDto>(command);

            var result = await _client.ChangeContentAsync(dto);

            return result;
        }

        public async Task<Guid> CreateAsync(CreateToDoCommand command)
        {
            var dto = _mapper.Map<CreateForManagerToDoDto>(command);

            var result = await _client.CreateAsync(dto);

            return result.Id;
        }

        public async Task<HttpResponseMessage> DeleteAsync(DeleteToDoCommand command)
        {
            var dto = _mapper.Map<DeleteToDoDto>(command);

            var result = await _client.DeleteAsync(dto);

            return result;
        }
    }
}
