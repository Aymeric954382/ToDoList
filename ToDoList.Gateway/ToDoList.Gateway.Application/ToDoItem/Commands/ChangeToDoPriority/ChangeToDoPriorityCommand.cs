using AutoMapper;
using ToDoList.Gateway.Application.Interfaces.Command_QuerySplitter;
using ToDoList.Gateway.Application.Interfaces.MappingMark;
using ToDoList.Gateway.Application.ToDoItem.Commands.ChangeToDoContent;
using ToDoList.Gateway.Contracts.ApiClients.RequestDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.ValueObjects;

namespace ToDoList.Gateway.Application.ToDoItem.Commands.ChangeToDoPriority
{
    public class ChangeToDoPriorityCommand : IVoidCommand, IMapWith<ChangeToDoPriorityDto>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ToDoPriority? Priority { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ChangeToDoPriorityCommand, ChangeToDoPriorityDto>();
        }
    }
}
