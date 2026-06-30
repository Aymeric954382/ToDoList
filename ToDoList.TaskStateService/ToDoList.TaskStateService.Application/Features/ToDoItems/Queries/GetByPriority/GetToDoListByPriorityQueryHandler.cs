using AutoMapper;
using MediatR;
using Serilog;
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
        private readonly ILogger _logger;

        public GetToDoListByPriorityQueryHandler(
            IToDoRepository repository,
            IMapper mapper,
            ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServiceResult<GetToDoListByPriorityResponseDto>> Handle(
            GetToDoListByPriorityQuery request,
            CancellationToken cancellationToken)
        {
            _logger.Information(
                "GetToDoListByPriority started. UserId={UserId}, Priority={Priority}",
                request.UserId,
                request.Priority);

            var filter = new ToDoFilter
            {
                UserId = request.UserId,
                Priority = request.Priority
            };

            try
            {
                var resultFiltered = await _repository
                    .GetByFilterAsync(filter, cancellationToken);

                var itemsDto = _mapper
                    .Map<List<ToDoItemDto>>(resultFiltered);

                _logger.Information(
                    "GetToDoListByPriority success. UserId={UserId}, Priority={Priority}, Count={Count}",
                    request.UserId,
                    request.Priority,
                    itemsDto.Count);

                var response = new GetToDoListByPriorityResponseDto
                {
                    Items = itemsDto
                };

                return ServiceResult<GetToDoListByPriorityResponseDto>
                    .Success(response);
            }
            catch (Exception ex)
            {
                _logger.Error(
                    ex,
                    "GetToDoListByPriority failed. UserId={UserId}, Priority={Priority}",
                    request.UserId,
                    request.Priority);

                return ServiceResult<GetToDoListByPriorityResponseDto>
                    .Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}