using ToDoList.Gateway.Application.Interfaces.Command_QuerySplitter;
using ToDoList.Gateway.Application.ToDoItem.Queries.Containers;
using ToDoList.Gateway.Contracts.ApiClients.ValueObjects;

namespace ToDoList.Gateway.Application.ToDoItem.Queries.GetByPriority
{
    public class GetToDoListByPriorityQuery : IQuery<ToDoListContainer>
    {
        public Guid UserId { get; set; }
        public ToDoPriority? Priority { get; set; }
    }
}
