using AutoMapper;
using MediatR;
using ToDoList.TaskManager.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.TaskManager.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskManager.Application.Interfaces.Repository;

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
                var items = await _repository.GetListByUserIdAsync(request.UserId ,cancellationToken);
                var itemsDto = _mapper.Map<List<ToDoItemDto>>(items);

                var response = new GetToDoListResponseDto()
                {
                    Items = itemsDto
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
