using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ToDoList.Application.Interfaces.Repository;
using ToDoList.Application.ToDoItems.Queries.Containers;
using ToDoList.Application.ToDoItems.Queries.Dtos;
using ToDoList.Application.ToDoItems.Queries.GetStatusQueries.GetToDoExpiringSoonStatus;
using ToDoList.Domain.ToDo.ValueObjects;

namespace ToDoList.Application.ToDoItems.Queries.GetStatusQueries.GetToDoExpiringSoonStatus
{
    public class GetToDoExpiringSoonStatusQueryHandler : IRequestHandler<GetToDoExpiringSoonStatusQuery, ToDoListContainer>
    {
        private readonly IToDoRepository _repository;
        private readonly IMapper _mapper;

        public GetToDoExpiringSoonStatusQueryHandler(IMapper mapper, IToDoRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ToDoListContainer> Handle(GetToDoExpiringSoonStatusQuery request, CancellationToken cancellationToken)
        {
            var query = _repository.AsQueryable()
                .Where(i => i.UserId == request.UserId && i.Status == ToDoStatus.ExpiringSoon);

            var itemsDto = await query.ProjectTo<ToDoDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ToDoListContainer { ToDoItems = itemsDto };
        }
    }
}