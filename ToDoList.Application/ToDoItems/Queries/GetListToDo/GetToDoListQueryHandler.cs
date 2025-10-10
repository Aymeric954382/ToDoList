using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Application.Common.Exceptions;
using ToDoList.Application.Interfaces.Repository;
using ToDoList.Application.ToDoItems.Queries.Containers;
using ToDoList.Application.ToDoItems.Queries.Dtos;
using ToDoList.Domain.ToDo;

namespace ToDoList.Application.ToDoItems.Queries.GetListToDo
{
    public class GetToDoListQueryHandler : IRequestHandler<GetToDoListQuery, ToDoListContainer>
    {
        private readonly IToDoRepository _repository;
        private readonly IMapper _mapper;
        public GetToDoListQueryHandler(IMapper mapper, IToDoRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<ToDoListContainer> Handle(GetToDoListQuery request, CancellationToken cancellationToken)
        {
            var query = _repository.AsQueryable()
                .Where(i => i.UserId == request.UserId);

            var itemsDto = await query.ProjectTo<ToDoDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ToDoListContainer {ToDoItems = itemsDto };
        }
    }
}
