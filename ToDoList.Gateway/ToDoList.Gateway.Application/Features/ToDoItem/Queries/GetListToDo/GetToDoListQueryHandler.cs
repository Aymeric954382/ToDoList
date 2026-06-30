using MediatR;
using Serilog;
using ToDoList.Gateway.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Interfaces.Orchestartors;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetListToDo
{
    public class GetToDoListQueryHandler
        : IRequestHandler<GetToDoListQuery,
            ServiceResult<GetToDoListResponseDto>>
    {
        private readonly IGetToDoListOrchestrator _orchestrator;
        private readonly ILogger _logger;

        public GetToDoListQueryHandler(
            IGetToDoListOrchestrator orchestrator,
            ILogger logger)
        {
            _orchestrator = orchestrator;
            _logger = logger;
        }

        public async Task<ServiceResult<GetToDoListResponseDto>> Handle(
            GetToDoListQuery request,
            CancellationToken cancellationToken)
        {
            _logger.Information("GetToDoList started");

            try
            {
                var result = await _orchestrator.GetListAsync(
                    request,
                    cancellationToken);

                _logger.Information("GetToDoList completed successfully");

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "GetToDoList failed");

                return ServiceResult<GetToDoListResponseDto>
                    .Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}