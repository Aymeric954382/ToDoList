using ToDoList.TaskStateService.Application.Features.ToDoItems.Queries.Contatiners;

namespace ToDoList.TaskStateService.Application.Features.ToDoItems.Queries.GetByOverdueToDos
{
    public class GetToDoListByOverdueResponseDto
    {
        public IEnumerable<ToDoItemDto> Items { get; set; }
    }
}