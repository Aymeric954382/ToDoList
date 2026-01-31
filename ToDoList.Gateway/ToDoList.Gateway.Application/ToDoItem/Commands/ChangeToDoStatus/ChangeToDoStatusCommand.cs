using AutoMapper;
using ToDoList.Gateway.Application.Interfaces.Command_QuerySplitter;
using ToDoList.Gateway.Application.Interfaces.MappingMark;
using ToDoList.Gateway.Application.ToDoItem.Commands.ChangeToDoContent;
using ToDoList.Gateway.Contracts.ApiClients.RequestDtos.Change;
using ToDoList.Gateway.Contracts.ApiClients.ValueObjects;

namespace ToDoList.Gateway.Application.ToDoItem.Commands.ChangeToDoStatus
{
    public class ChangeToDoStatusCommand : IVoidCommand, IMapWith<ChangeToDoStatusDto>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ToDoStatus Status { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ChangeToDoStatusCommand, ChangeToDoStatusDto>();
        }
    }
}
