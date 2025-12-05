using AutoMapper;
using ToDoList.TaskManager.Application.Interfaces.MappingMark;
using ToDoList.TaskManager.Domain;
using ToDoList.TaskManager.Domain.ValueObjects;

namespace ToDoList.TaskManager.Application.ToDoItems.Queries.ResponseDtos
{
    public class ToDoResponseDto : IMapWith<ToDoItem>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime? DueDate { get; set; }
        public ToDoStatus Status { get; set; }
        public ToDoPriority? Priority { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<ToDoItem, ToDoResponseDto>();

    }
}
