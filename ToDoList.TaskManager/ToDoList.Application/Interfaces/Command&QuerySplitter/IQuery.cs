using MediatR;

namespace ToDoList.Application.Interfaces.Command_QuerySpliter
{
    public interface IQuery<T> : IRequest<T> { }
}
