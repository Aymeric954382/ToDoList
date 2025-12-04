using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoList.TaskStateService.Application.Interfaces.Repository;
using ToDoList.TaskStateService.Application.ToDoItems.Queries.Contatiners;
using ToDoList.TaskStateService.Application.ToDoItems.Queries.ResponseDtos;
using ToDoList.TaskStateService.Domain.ValueObjects;

namespace ToDoList.TaskStateService.Application.ToDoItems.Queries.GetByOverdueToDos
{
    public class GetToDoListOverdueQueryHandler : IRequestHandler<GetToDoListOverdueQuery, ToDoListContainer>
    {
        public readonly IToDoRepository _repository;

        public readonly IMapper _mapper;
        public GetToDoListOverdueQueryHandler(IToDoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ToDoListContainer> Handle(GetToDoListOverdueQuery request, CancellationToken cancellationToken)
        {
            var query = _repository.AsQueryable()
                .Where(i => i.UserId == request.UserId &&
                i.DueDate <= DateTime.UtcNow &&
                i.Status != ToDoStatus.Completed &&
                i.Status != ToDoStatus.Cancelled);

            var listDto = await query.ProjectTo<ToDoResponseDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ToDoListContainer { ToDoItems = listDto };
        }
    }
}
