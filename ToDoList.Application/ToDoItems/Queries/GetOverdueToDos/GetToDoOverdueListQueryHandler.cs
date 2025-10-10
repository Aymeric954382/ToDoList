using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Interfaces.Repository;
using ToDoList.Application.ToDoItems.Queries.Containers;
using ToDoList.Application.ToDoItems.Queries.Dtos;
using ToDoList.Domain.ToDo.ValueObjects;

namespace ToDoList.Application.ToDoItems.Queries.GetOverdueToDos
{
    public class GetToDoOverdueListQueryHandler : IRequestHandler<GetToDoOverdueListQuery, ToDoListContainer>
    {
        public readonly IToDoRepository _repository;

        public readonly IMapper _mapper;
        public GetToDoOverdueListQueryHandler(IToDoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ToDoListContainer> Handle(GetToDoOverdueListQuery request, CancellationToken cancellationToken)
        {
            var query = _repository.AsQueryable()
                .Where(i => i.UserId == request.UserId && 
                i.DueDate <= DateTime.UtcNow &&
                i.Status != ToDoStatus.Completed);

            var listDto = await query.ProjectTo<ToDoDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ToDoListContainer { ToDoItems = listDto };
        }
    }
}
