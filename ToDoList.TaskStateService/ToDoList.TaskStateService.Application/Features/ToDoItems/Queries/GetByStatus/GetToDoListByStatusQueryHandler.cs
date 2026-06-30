using AutoMapper;
using MediatR;
using Serilog;
using ToDoList.TaskStateService.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.TaskStateService.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskStateService.Application.Features.ToDoItems.Filter;
using ToDoList.TaskStateService.Application.Features.ToDoItems.Queries.Contatiners;
using ToDoList.TaskStateService.Application.Interfaces.Repository;

namespace ToDoList.TaskStateService.Application.Features.ToDoItems.Queries.GetByStatus
{
    public class GetToDoListByStatusQueryHandler
        : IRequestHandler<GetToDoListByStatusQuery,
            ServiceResult<GetToDoListByStatusResponseDto>>
    {
        private readonly IToDoRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetToDoListByStatusQueryHandler(
            IToDoRepository repository,
            IMapper mapper,
            ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServiceResult<GetToDoListByStatusResponseDto>> Handle(
            GetToDoListByStatusQuery request,
            CancellationToken cancellationToken)
        {
            _logger.Information(
                "GetToDoListByStatus started. UserId={UserId}, Status={Status}",
                request.UserId,
                request.Status);

            var filter = new ToDoFilter
            {
                UserId = request.UserId,
                Status = request.Status
            };

            try
            {
                var resultFiltered = await _repository
                    .GetByFilterAsync(filter, cancellationToken);

                var itemsDto = _mapper
                    .Map<List<ToDoItemDto>>(resultFiltered);

                _logger.Information(
                    "GetToDoListByStatus success. UserId={UserId}, Status={Status}, Count={Count}",
                    request.UserId,
                    request.Status,
                    itemsDto.Count);

                var response = new GetToDoListByStatusResponseDto
                {
                    Items = itemsDto
                };

                return ServiceResult<GetToDoListByStatusResponseDto>
                    .Success(response);
            }
            catch (Exception ex)
            {
                _logger.Error(
                    ex,
                    "GetToDoListByStatus failed. UserId={UserId}, Status={Status}",
                    request.UserId,
                    request.Status);

                return ServiceResult<GetToDoListByStatusResponseDto>
                    .Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}