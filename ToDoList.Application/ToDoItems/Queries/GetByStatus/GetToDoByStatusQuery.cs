using System;
using ToDoList.Application.Interfaces.Command_QuerySpliter;
using ToDoList.Application.ToDoItems.Queries.Containers;
using ToDoList.Domain.ToDo.ValueObjects;

namespace ToDoList.Application.ToDoItems.Queries.GetByStatus
{
    public class GetToDoByStatusQuery : IQuery<ToDoListContainer>
    {
        public Guid UserId { get; set; }
        public ToDoStatus Status { get; set; }
    }
}