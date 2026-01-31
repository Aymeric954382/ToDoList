using AutoMapper;
using ToDoList.Gateway.Application.Interfaces.Command_QuerySplitter;
using ToDoList.Gateway.Application.Interfaces.MappingMark;
using ToDoList.Gateway.Application.ToDoItem.Commands.ChangeToDoContent;
using ToDoList.Gateway.Contracts.ApiClients.RequestDtos.Delete;

namespace ToDoList.Gateway.Application.ToDoItem.Commands.DeleteToDo
{
    public class DeleteToDoCommand : IVoidCommand, IMapWith<GetToDoListOverdueDto>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteToDoCommand, GetToDoListOverdueDto>();
        }
    }
}
