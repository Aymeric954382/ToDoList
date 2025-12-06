using MediatR;

namespace ToDoList.TaskManager.Application.Interfaces.Command_QuerySpliter
{
    public interface IQuery<T> : IRequest<T> { }
}
