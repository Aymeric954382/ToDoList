using AutoMapper;
using MediatR;
using Serilog;
using ToDoList.TaskStateService.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.TaskStateService.Application.Features.ResponseServiceResultsContainer;
using ToDoList.TaskStateService.Application.Features.ToDoItems.Queries.Contatiners;
using ToDoList.TaskStateService.Application.Interfaces.Repository;

namespace ToDoList.TaskStateService.Application.Features.ToDoItems.Queries.GetListToDo
{
    public class GetToDoListQueryHandler
        : IRequestHandler<GetToDoListQuery,
            ServiceResult<GetToDoListResponseDto>>
    {
        private readonly IToDoRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetToDoListQueryHandler(
            IToDoRepository repository,
            IMapper mapper,
            ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServiceResult<GetToDoListResponseDto>> Handle(
            GetToDoListQuery request,
            CancellationToken cancellationToken)
        {
            _logger.Information(
                "GetToDoList started. UserId={UserId}",
                request.UserId);

            try
            {
                var result = await _repository
                    .GetListByUserIdAsync(request.UserId, cancellationToken);

                var itemsDto = _mapper
                    .Map<List<ToDoItemDto>>(result);

                _logger.Information(
                    "GetToDoList success. UserId={UserId}, Count={Count}",
                    request.UserId,
                    itemsDto.Count);

                var response = new GetToDoListResponseDto
                {
                    Items = itemsDto
                };

                return ServiceResult<GetToDoListResponseDto>
                    .Success(response);
            }
            catch (Exception ex)
            {
                _logger.Error(
                    ex,
                    "GetToDoList failed. UserId={UserId}",
                    request.UserId);

                return ServiceResult<GetToDoListResponseDto>
                    .Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}