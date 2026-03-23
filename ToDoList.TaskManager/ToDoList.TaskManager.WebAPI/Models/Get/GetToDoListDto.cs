using ToDoList.TaskManager.Domain;

namespace ToDoList.TaskManager.WebAPI.Models.Get
{
    public class GetToDoListDto
    {
        public IEnumerable<ToDoItem> Items { get; set; }
    }
}
