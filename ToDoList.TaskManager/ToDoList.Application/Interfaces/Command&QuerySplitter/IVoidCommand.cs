using MediatR;

namespace ToDoList.Application.Interfaces.Command_QuerySpliter
{
    public interface IVoidCommand : IRequest<Unit> { }
}
