using AutoMapper;
using ToDoList.TaskStateService.Application.Interfaces.MappingMark;
using ToDoList.TaskStateService.Domain;
using ToDoList.TaskStateService.Domain.ValueObjects;

namespace ToDoList.TaskStateService.Application.ToDoItems.Queries.ResponseDtos
{
    public class ToDoResponseDto : IMapWith<ToDoItem>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime? DueDate { get; set; }
        public ToDoStatus Status { get; set; }
        public ToDoPriority? Priority { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<ToDoItem, ToDoResponseDto>();

    }
}
