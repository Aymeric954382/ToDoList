using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Worker.Application.Interfaces.Repository;
using ToDoList.Worker.Application.ToDoItems.Queries.Contatiners;
using ToDoList.Worker.Application.ToDoItems.Queries.ResponseDtos;

namespace ToDoList.Worker.Application.ToDoItems.Queries.GetListToDo
{
    public class GetToDoListQueryHandler : IRequestHandler<GetToDoListQuery, ToDoListContainer>
    {
        private readonly IToDoRepository _repository;
        private readonly IMapper _mapper;
        public GetToDoListQueryHandler(IToDoRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<ToDoListContainer> Handle(GetToDoListQuery request, CancellationToken cancellationToken)
        {
            var query = _repository.AsQueryable()
                .Where(i => i.UserId == request.UserId);

            var itemsDto = await query.ProjectTo<ToDoResponseDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ToDoListContainer { ToDoItems = itemsDto };
        }
    }
}

