using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoList.TaskStateService.Application.Interfaces.Repository;
using ToDoList.TaskStateService.Application.ToDoItems.Queries.Contatiners;
using ToDoList.TaskStateService.Application.ToDoItems.Queries.ResponseDtos;

namespace ToDoList.TaskStateService.Application.ToDoItems.Queries.GetByStatus
{
    public class GetToDoListByStatusQueryHandler : IRequestHandler<GetToDoListByStatusQuery, ToDoListContainer>
    {
        private readonly IToDoRepository _repository;
        private readonly IMapper _mapper;

        public GetToDoListByStatusQueryHandler(IToDoRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ToDoListContainer> Handle(GetToDoListByStatusQuery request, CancellationToken cancellationToken)
        {
            var query = _repository.AsQueryable()
                .Where(i => i.UserId == request.UserId && i.Status == request.Status);

            var itemsDto = await query.ProjectTo<ToDoResponseDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ToDoListContainer { ToDoItems = itemsDto };
        }
    }
}
