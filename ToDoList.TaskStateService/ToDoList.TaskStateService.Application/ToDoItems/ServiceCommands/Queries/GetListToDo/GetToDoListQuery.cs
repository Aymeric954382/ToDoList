using ToDoList.TaskStateService.Application.Interfaces.Command_QuerySplitter;
using ToDoList.TaskStateService.Application.ToDoItems.Queries.Contatiners;

namespace ToDoList.TaskStateService.Application.ToDoItems.Queries.GetListToDo
{
    public class GetToDoListQuery : IQuery<ToDoListContainer>
    {
        public Guid UserId { get; set; }
    }
}
