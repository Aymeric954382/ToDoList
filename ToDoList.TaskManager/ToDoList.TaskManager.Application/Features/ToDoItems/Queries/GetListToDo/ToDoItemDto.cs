using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.TaskManager.Application.Interfaces.MappingMark;
using ToDoList.TaskManager.Domain;

namespace ToDoList.TaskManager.Application.Features.ToDoItems.Queries.GetListToDo
{
    public class ToDoItemDto : IMapWith<ToDoItem>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Details { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ToDoItem, ToDoItemDto>();
        }
    }
}
