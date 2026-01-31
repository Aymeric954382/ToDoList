using AutoMapper;
using ToDoList.Gateway.Application.Interfaces.Command_QuerySplitter;
using ToDoList.Gateway.Application.Interfaces.MappingMark;
using ToDoList.Gateway.Contracts.ApiClients.RequestDtos.Change;

namespace ToDoList.Gateway.Application.ToDoItem.Commands.ChangeToDoContent
{
    public class ChangeToDoContentCommand : IVoidCommand, IMapWith<ChangeToDoContentDto>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ChangeToDoContentCommand, ChangeToDoContentDto>();
        }
    }

}
