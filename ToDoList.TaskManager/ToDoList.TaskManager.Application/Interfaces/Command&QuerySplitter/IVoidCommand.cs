using MediatR;

namespace ToDoList.TaskManager.Application.Interfaces.Command_QuerySpliter
{
    public interface IVoidCommand : IRequest<Unit> { }
}
