using System;
using ToDoList.Application.Interfaces.Command_QuerySpliter;
using ToDoList.Application.ToDoItems.Queries.Containers;

namespace ToDoList.Application.ToDoItems.Queries.GetStatusQueries.GetToDoCancelledStatus
{
    public class GetToDoCancelledStatusQuery : IQuery<ToDoListContainer>
    {
        public Guid UserId { get; set; }
    }
}