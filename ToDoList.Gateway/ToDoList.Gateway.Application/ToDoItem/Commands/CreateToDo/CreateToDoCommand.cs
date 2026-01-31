using AutoMapper;
using ToDoList.Gateway.Application.Interfaces.Command_QuerySplitter;
using ToDoList.Gateway.Application.Interfaces.MappingMark;
using ToDoList.Gateway.Application.ToDoItem.Commands.ChangeToDoContent;
using ToDoList.Gateway.Contracts.ApiClients.RequestDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.ValueObjects;

namespace ToDoList.Gateway.Application.ToDoItem.Commands.CreateToDo
{
    public class CreateToDoCommand : IWithResultCommand<Guid>, IMapWithTwo<CreateForManagerToDoDto, CreateForServiceToDoDto>
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime? DueDate { get; set; }
        public ToDoPriority? Priority { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateToDoCommand, CreateForManagerToDoDto>();
            profile.CreateMap<CreateToDoCommand, CreateForServiceToDoDto>();
        }
    }
}
