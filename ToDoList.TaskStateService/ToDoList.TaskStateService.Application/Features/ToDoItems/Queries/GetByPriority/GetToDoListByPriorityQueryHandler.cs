using AutoMapper;
using MediatR;
using ToDoList.TaskStateService.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.TaskStateService.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskStateService.Application.Features.ToDoItems.Filter;
using ToDoList.TaskStateService.Application.Features.ToDoItems.Queries.Contatiners;
using ToDoList.TaskStateService.Application.Interfaces.Repository;

namespace ToDoList.TaskStateService.Application.Features.ToDoItems.Queries.GetByPriority
{
    public class GetToDoListByPriorityQueryHandler 
        : IRequestHandler<GetToDoListByPriorityQuery, 
        ServiceResult<GetToDoListByPriorityResponseDto>>
    {
        private readonly IToDoRepository _repository;
        private readonly IMapper _mapper;

        public GetToDoListByPriorityQueryHandler(IToDoRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ServiceResult<GetToDoListByPriorityResponseDto>> Handle(
            GetToDoListByPriorityQuery request, 
            CancellationToken cancellationToken)
        {
            var filtered = new ToDoFilter()
            {
                Priority = request.Priority
            };

            try
            {
                var resultFiltered = await _repository.GetByFilterAsync(filtered, cancellationToken);

                var itemsDto = _mapper.Map<List<ToDoItemDto>>(resultFiltered);

                var response = new GetToDoListByPriorityResponseDto()
                {
                    Items = itemsDto
                };

                return ServiceResult<GetToDoListByPriorityResponseDto>
                    .Success(response);
            }
            catch (Exception ex)
            {
                //logger

                return ServiceResult<GetToDoListByPriorityResponseDto>
                    .Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}
