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
using ToDoList.Worker.Domain.ValueObjects;

namespace ToDoList.Worker.Application.ToDoItems.Queries.GetOverdueToDos
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
