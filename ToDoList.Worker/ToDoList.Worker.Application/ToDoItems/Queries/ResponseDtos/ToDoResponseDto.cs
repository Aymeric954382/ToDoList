using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Worker.Application.Interfaces.MappingMark;
using ToDoList.Worker.Domain;
using ToDoList.Worker.Domain.ValueObjects;

namespace ToDoList.Worker.Application.ToDoItems.Queries.ResponseDtos
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
