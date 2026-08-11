using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.Handlers.CreateToDo;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Create;

namespace ToDoList.Gateway.Application.Interfaces.Orchestartors
{
    public interface ICreateToDoWorkflow
    {
        Task CreateAsync(CreateToDoCommand command, CancellationToken cancellationToken);
    }
}
