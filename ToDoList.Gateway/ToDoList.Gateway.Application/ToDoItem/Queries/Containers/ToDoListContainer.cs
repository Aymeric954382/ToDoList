using ToDoList.Gateway.Application.ToDoItem.ResponseDtos;

namespace ToDoList.Gateway.Application.ToDoItem.Queries.Containers
{
    public class ToDoListContainer
    {
        public IList<ToDoResponseDto> ToDoItems { get; set; }
    }
}
