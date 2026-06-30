using AutoMapper;
using MediatR;
using Serilog;
using ToDoList.TaskStateService.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.TaskStateService.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskStateService.Application.Features.ToDoItems.Filter;
using ToDoList.TaskStateService.Application.Features.ToDoItems.Queries.Contatiners;
using ToDoList.TaskStateService.Application.Interfaces.Repository;

namespace ToDoList.TaskStateService.Application.Features.ToDoItems.Queries.GetByOverdueToDos
{
    public class GetToDoListByOverdueQueryHandler
        : IRequestHandler<GetToDoListOverdueQuery,
            ServiceResult<GetToDoListByOverdueResponseDto>>
    {
        private readonly IToDoRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetToDoListByOverdueQueryHandler(
            IToDoRepository repository,
            IMapper mapper,
            ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServiceResult<GetToDoListByOverdueResponseDto>> Handle(
            GetToDoListOverdueQuery request,
            CancellationToken cancellationToken)
        {
            _logger.Information(
                "GetOverdueToDos started. UserId={UserId}",
                request.UserId);

            var filter = new ToDoFilter
            {
                UserId = request.UserId,
                IsOverdue = true
            };

            try
            {
                var resultFiltered = await _repository
                    .GetByFilterAsync(filter, cancellationToken);

                var itemsDto = _mapper
                    .Map<List<ToDoItemDto>>(resultFiltered);

                _logger.Information(
                    "GetOverdueToDos success. UserId={UserId}, Count={Count}",
                    request.UserId,
                    itemsDto.Count);

                var response = new GetToDoListByOverdueResponseDto
                {
                    Items = itemsDto
                };

                return ServiceResult<GetToDoListByOverdueResponseDto>
                    .Success(response);
            }
            catch (Exception ex)
            {
                _logger.Error(
                    ex,
                    "GetOverdueToDos failed. UserId={UserId}",
                    request.UserId);

                return ServiceResult<GetToDoListByOverdueResponseDto>
                    .Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}