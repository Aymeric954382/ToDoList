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
using ToDoList.Domain.ToDo.ValueObjects;

namespace ToDoList.Application.ToDoItems.Queries.GetStatusQueries.GetToDoActiveStatus
{
    public class GetToDoActiveStatusQueryHandler : IRequestHandler<GetToDoActiveStatusQuery, ToDoListContainer>
    {
        private readonly IToDoRepository _repository;
        private readonly IMapper _mapper;

        public GetToDoActiveStatusQueryHandler(IMapper mapper, IToDoRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ToDoListContainer> Handle(GetToDoActiveStatusQuery request, CancellationToken cancellationToken)
        {
            var query = _repository.AsQueryable()
                .Where(i => i.UserId == request.UserId && i.Status == ToDoStatus.Active);

            var itemsDto = await query.ProjectTo<ToDoDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ToDoListContainer { ToDoItems = itemsDto };
        }
    }
}