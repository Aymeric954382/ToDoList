using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.TaskStateService.Application.Interfaces.Command_QuerySplitter
{
    public interface IVoidCommand : IRequest<Unit> { }

}
