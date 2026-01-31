using AutoMapper;
using ToDoList.Gateway.Application.Interfaces.Command_QuerySplitter;
using ToDoList.Gateway.Application.Interfaces.MappingMark;
using ToDoList.Gateway.Application.ToDoItem.Commands.ChangeToDoContent;
using ToDoList.Gateway.Contracts.ApiClients.RequestDtos.Change;

namespace ToDoList.Gateway.Application.ToDoItem.Commands.ChangeToDoDueDate
{
    public class ChangeToDoDueDateCommand : IVoidCommand, IMapWith<ChangeToDoDueDateDto>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime? DueDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ChangeToDoDueDateCommand, ChangeToDoDueDateDto>();
        }
    }
}
