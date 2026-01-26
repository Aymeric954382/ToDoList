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

namespace ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter
{
    public interface ITaskStateServiceApiClientAdapter
    {
        public Task<HttpResponseMessage> ChangeDueDateAsync(ChangeToDoDueDateCommand command);
        public Task<HttpResponseMessage> ChangeStatusAsync(ChangeToDoStatusCommand command);
        public Task<HttpResponseMessage> ChangePriorityAsync(ChangeToDoPriorityCommand command);
        public Task<HttpResponseMessage> CreateAsync(CreateToDoCommand command, Guid id);
        public Task<HttpResponseMessage> DeleteAsync(DeleteToDoCommand command);
    }
}
