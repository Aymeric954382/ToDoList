using ToDoList.TaskStateService.Application.ToDoItems.Queries.ResponseDtos;

namespace ToDoList.TaskStateService.Application.ToDoItems.Queries.Contatiners
{
    public class ToDoListContainer
    {
        public IList<ToDoResponseDto> ToDoItems { get; set; }
    }
}
