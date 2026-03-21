using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoList.TaskManager.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.TaskManager.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskManager.Application.Interfaces.Repository;
using ToDoList.TaskManager.Domain;

namespace ToDoList.TaskManager.Application.Features.ToDoItems.Queries.GetListToDo
{
    public class GetToDoListQueryHandler : IRequestHandler<GetToDoListQuery, ServiceResult<GetToDoListResponseDto>>
    {
        private readonly IToDoRepository _repository;
        private readonly IMapper _mapper;
        public GetToDoListQueryHandler(IToDoRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<ServiceResult<GetToDoListResponseDto>> Handle(
            GetToDoListQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var query = _repository.AsQueryable()
                    .Where(i => i.UserId == request.UserId);

                var itemsDto = await query.ProjectTo<ToDoItem>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                var response = new GetToDoListResponseDto()
                {
                    Items = itemsDto.Select(x => new ToDoItem()
                    {
                        Id = x.Id,
                        UserId = x.UserId,
                        Details = x.Details,
                        Title = x.Title
                    })
                };

                return ServiceResult<GetToDoListResponseDto>.Success(response);
            }
            catch(Exception ex)
            {
                //logger

                return ServiceResult<GetToDoListResponseDto>.Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}
