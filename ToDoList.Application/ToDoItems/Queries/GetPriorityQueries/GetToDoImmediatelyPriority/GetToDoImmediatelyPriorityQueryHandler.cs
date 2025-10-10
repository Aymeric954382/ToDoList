using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Application.Interfaces.Repository;
using ToDoList.Application.ToDoItems.Queries.Containers;
using ToDoList.Application.ToDoItems.Queries.Dtos;
using ToDoList.Application.ToDoItems.Queries.GetToDoHighPriority;
using ToDoList.Domain.ToDo.ValueObjects;

namespace ToDoList.Application.ToDoItems.Queries.GetPriorityQueries.GetToDoImmediatelyPriority
{
    public class GetToDoMediumPriorityQueryHandler : IRequestHandler<GetToDoImmediatelyPriorityQuery, ToDoListContainer>
    {
        private readonly IToDoRepository _repository;

        private readonly IMapper _mapper;

        public GetToDoMediumPriorityQueryHandler(IToDoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ToDoListContainer> Handle(GetToDoImmediatelyPriorityQuery request, CancellationToken cancellationToken)
        {
            var query = _repository.AsQueryable()
                .Where(i => i.UserId == request.UserId && i.Priority == ToDoPriority.Immediately);

            var listDto = await query.ProjectTo<ToDoDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new ToDoListContainer { ToDoItems = listDto };
        }
    }
}
