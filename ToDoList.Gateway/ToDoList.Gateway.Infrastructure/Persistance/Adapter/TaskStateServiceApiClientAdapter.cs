using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using ToDoList.Gateway.Application.ToDoItem.Commands.ChangeToDoContent;
using ToDoList.Gateway.Application.ToDoItem.Commands.ChangeToDoDueDate;
using ToDoList.Gateway.Application.ToDoItem.Commands.ChangeToDoPriority;
using ToDoList.Gateway.Application.ToDoItem.Commands.ChangeToDoStatus;
using ToDoList.Gateway.Application.ToDoItem.Commands.CreateToDo;
using ToDoList.Gateway.Application.ToDoItem.Commands.DeleteToDo;
using ToDoList.Gateway.Contracts.ApiClients.Interfaces;
using ToDoList.Gateway.Contracts.ApiClients.RequestDtos;

namespace ToDoList.Gateway.Infrastructure.Persistance.Adapter
{
    public class TaskStateServiceApiClientAdapter : ITaskStateServiceApiClientAdapter
    {
        private readonly ITaskStateServiceApiClient _client;
        private readonly IMapper _mapper;
        public TaskStateServiceApiClientAdapter(ITaskStateServiceApiClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<HttpResponseMessage> ChangeDueDateAsync(ChangeToDoDueDateCommand command)
        {
            var dto = _mapper.Map<ChangeToDoDueDateDto>(command);

            var result = await _client.ChangeDueDateAsync(dto);

            return result;
        }

        public async Task<HttpResponseMessage> ChangeStatusAsync(ChangeToDoStatusCommand command)
        {
            var dto = _mapper.Map<ChangeToDoStatusDto>(command);

            var result = await _client.ChangeStatusAsync(dto);

            return result;
        }

        public async Task<HttpResponseMessage> ChangePriorityAsync(ChangeToDoPriorityCommand command)
        {
            var dto = _mapper.Map<ChangeToDoPriorityDto>(command);

            var result = await _client.ChangePriorityAsync(dto);

            return result;
        }

        public async Task<HttpResponseMessage> CreateAsync(CreateToDoCommand command, Guid id)
        {
            var dto = _mapper.Map<CreateForServiceToDoDto>(command);

            dto.Id = id;

            var result = await _client.CreateAsync(dto);

            return result;
        }

        public async Task<HttpResponseMessage> DeleteAsync(DeleteToDoCommand command)
        {
            var dto = _mapper.Map<DeleteToDoDto>(command);

            var result = await _client.DeleteAsync(dto);

            return result;
        }
    }
}
