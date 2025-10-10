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
using ToDoList.Domain.ToDo.ValueObjects;

namespace ToDoList.Application.ToDoItems.Queries.GetPriorityQueries.GetToDoLowPriority
{
    public class GetToDoLowPriorityQueryHandler : IRequestHandler<GetToDoLowPriorityQuery, ToDoListContainer>
    {
        private readonly IToDoRepository _repository;

        private readonly IMapper _mapper;

        public GetToDoLowPriorityQueryHandler(IToDoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ToDoListContainer> Handle(GetToDoLowPriorityQuery request, CancellationToken cancellationToken)
        {
            var query = _repository.AsQueryable()
                .Where(i => i.UserId == request.UserId && i.Priority == ToDoPriority.High);

            var listDto = await query.ProjectTo<ToDoDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new ToDoListContainer { ToDoItems = listDto };
        }
    }
}
