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

namespace ToDoList.Application.ToDoItems.Queries.GetStatusQueries.GetToDoCancelledStatus
{
    public class GetToDoCancelledStatusQueryHandler : IRequestHandler<GetToDoCancelledStatusQuery, ToDoListContainer>
    {
        private readonly IToDoRepository _repository;
        private readonly IMapper _mapper;

        public GetToDoCancelledStatusQueryHandler(IMapper mapper, IToDoRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ToDoListContainer> Handle(GetToDoCancelledStatusQuery request, CancellationToken cancellationToken)
        {
            var query = _repository.AsQueryable()
                .Where(i => i.UserId == request.UserId && i.Status == ToDoStatus.Cancelled);

            var itemsDto = await query.ProjectTo<ToDoDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ToDoListContainer { ToDoItems = itemsDto };
        }
    }
}