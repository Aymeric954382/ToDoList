using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Application.ToDoItem.Commands.ChangeToDoContent;
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
using ToDoList.Gateway.Contracts.ApiClients.RequestDtos.Get;
using ToDoList.Gateway.Contracts.ApiClients.ResponseDtos;

namespace ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter
{
    public interface ITaskStateServiceApiClientAdapter
    {
        //command
        public Task<HttpResponseMessage> ChangeDueDateAsync(ChangeToDoDueDateCommand command);
        public Task<HttpResponseMessage> ChangeStatusAsync(ChangeToDoStatusCommand command);
        public Task<HttpResponseMessage> ChangePriorityAsync(ChangeToDoPriorityCommand command);
        public Task<HttpResponseMessage> CreateAsync(CreateToDoCommand command, Guid id);
        public Task<HttpResponseMessage> DeleteAsync(DeleteToDoCommand command);

        //query
        public Task<ToDoListContainer> GetToDoListAsync(CriteriaSplitterQuery query);
        public Task<ToDoListContainer> GetToDoListByPriorityAsync(CriteriaSplitterQuery query);
        public Task<ToDoListContainer> GetToDoListByStatusAsync(CriteriaSplitterQuery query);
        public Task<ToDoListContainer> GetToDoListByOverdueAsync(CriteriaSplitterQuery query);
    }
}
