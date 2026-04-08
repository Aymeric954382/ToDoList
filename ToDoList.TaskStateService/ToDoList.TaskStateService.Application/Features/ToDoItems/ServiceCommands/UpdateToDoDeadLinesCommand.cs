using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.TaskStateService.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskStateService.Application.Interfaces.Command_QuerySplitter;

namespace ToDoList.TaskStateService.Application.Features.ToDoItems.ServiceCommands
{
    public class UpdateToDoDeadLinesCommand : IWithResultCommand<InternalServiceResult<UpdateToDoDeadLinesResponseDto>>
    {
        public IEnumerable<UpdateToDoDeadLinesContainer> Items { get; init; } 
    }
}
