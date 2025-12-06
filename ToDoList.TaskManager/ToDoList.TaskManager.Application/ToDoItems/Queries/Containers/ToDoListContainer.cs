using ToDoList.TaskManager.Application.ToDoItems.Queries.ResponseDtos;

namespace ToDoList.TaskManager.Application.ToDoItems.Queries.Containers
{
    public class ToDoListContainer
    {
        public IList<ToDoResponseDto> ToDoItems { get; set; }
    }
}
