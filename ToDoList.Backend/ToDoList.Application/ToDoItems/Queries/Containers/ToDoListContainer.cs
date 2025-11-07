using ToDoList.Application.ToDoItems.Queries.ResponseDtos;

namespace ToDoList.Application.ToDoItems.Queries.Containers
{
    public class ToDoListContainer
    {
        public IList<ToDoResponseDto> ToDoItems { get; set; }
    }
}
