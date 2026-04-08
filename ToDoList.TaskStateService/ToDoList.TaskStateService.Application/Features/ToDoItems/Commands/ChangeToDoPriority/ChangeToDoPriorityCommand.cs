using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.TaskStateService.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskStateService.Application.Interfaces.Command_QuerySplitter;
using ToDoList.TaskStateService.Application.Interfaces.MappingMark;
using ToDoList.TaskStateService.Domain.ValueObjects;

namespace ToDoList.TaskStateService.Application.Features.ToDoItems.Commands.ChangeToDoPriority
{
    public class ChangeToDoPriorityCommand 
        : IWithResultCommand<ServiceResult<ChangeToDoPriorityResponseDto>>,
            IMapWith<ChangeToDoPriorityResponseDto>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ToDoPriority? Priority { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ChangeToDoPriorityResponseDto, ChangeToDoPriorityCommand>();
        }
    }
}
