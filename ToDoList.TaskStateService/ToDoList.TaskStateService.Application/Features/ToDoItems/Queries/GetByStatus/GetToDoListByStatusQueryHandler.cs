using AutoMapper;
using MediatR;
using ToDoList.TaskStateService.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.TaskStateService.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskStateService.Application.Features.ToDoItems.Filter;
using ToDoList.TaskStateService.Application.Features.ToDoItems.Queries.Contatiners;
using ToDoList.TaskStateService.Application.Features.ToDoItems.Queries.GetByPriority;
using ToDoList.TaskStateService.Application.Interfaces.Repository;

namespace ToDoList.TaskStateService.Application.Features.ToDoItems.Queries.GetByStatus
{
    public class GetToDoListByStatusQueryHandler 
        : IRequestHandler<GetToDoListByStatusQuery, 
            ServiceResult<GetToDoListByStatusResponseDto>>
    {
        private readonly IToDoRepository _repository;
        private readonly IMapper _mapper;

        public GetToDoListByStatusQueryHandler(IToDoRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ServiceResult<GetToDoListByStatusResponseDto>> Handle(
            GetToDoListByStatusQuery request, 
            CancellationToken cancellationToken)
        {
            var filtered = new ToDoFilter()
            {
                Status = request.Status
            };

            try
            {
                var resultFiltered = await _repository.GetByFilterAsync(filtered, cancellationToken);

                var itemsDto = _mapper.Map<List<ToDoItemDto>>(resultFiltered);

                var response = new GetToDoListByStatusResponseDto()
                {
                    Items = itemsDto
                };

                return ServiceResult<GetToDoListByStatusResponseDto>
                    .Success(response);
            }
            catch (Exception ex)
            {
                //logger

                return ServiceResult<GetToDoListByStatusResponseDto>
                    .Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}
