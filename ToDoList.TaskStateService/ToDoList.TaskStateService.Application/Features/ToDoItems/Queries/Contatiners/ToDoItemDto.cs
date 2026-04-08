using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.TaskStateService.Application.Interfaces.MappingMark;
using ToDoList.TaskStateService.Domain;
using ToDoList.TaskStateService.Domain.ValueObjects;

namespace ToDoList.TaskStateService.Application.Features.ToDoItems.Queries.Contatiners
{
    public class ToDoItemDto : IMapWith<ToDoItem>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ToDoStatus Status { get; set; }
        public ToDoPriority? Priority { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? EditDate { get; set; }
        public DateTime? DueDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ToDoItem, ToDoItemDto>();
        }
    }
}
