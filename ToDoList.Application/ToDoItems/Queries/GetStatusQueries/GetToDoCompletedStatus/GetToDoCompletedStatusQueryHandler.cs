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
using ToDoList.Domain.ToDo;
using ToDoList.Domain.ToDo.ValueObjects;

namespace ToDoList.Application.ToDoItems.Queries.GetStatusQueries.GetToDoCompletedStatus
{
    public class GetToDoCompletedStatusQueryHandler : IRequestHandler<GetToDoCompletedStatusQuery, ToDoListContainer>
    {
        private readonly IToDoRepository _repository;
        private readonly IMapper _mapper;

        public GetToDoCompletedStatusQueryHandler(IMapper mapper, IToDoRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ToDoListContainer> Handle(GetToDoCompletedStatusQuery request, CancellationToken cancellationToken)
        {
            var query = _repository.AsQueryable()
                .Where(i => i.UserId == request.UserId && i.Status == ToDoStatus.Completed);

            var itemsDto = await query.ProjectTo<ToDoDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ToDoListContainer { ToDoItems = itemsDto };
        }
    }
}