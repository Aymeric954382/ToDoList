using System;
using ToDoList.Application.Interfaces.Command_QuerySpliter;
using ToDoList.Application.ToDoItems.Queries.Containers;

namespace ToDoList.Application.ToDoItems.Queries.GetStatusQueries.GetToDoCompletedStatus
{
    public class GetToDoCompletedStatusQuery : IQuery<ToDoListContainer>
    {
        public Guid UserId { get; set; }
    }
}