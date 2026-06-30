using MediatR;
using Serilog;
using ToDoList.Gateway.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;
using ToDoList.Gateway.Application.Interfaces.Orchestartors;

namespace ToDoList.Gateway.Application.Features.ToDoItem.Queries.GetOverdueToDo
{
    public class GetToDoListByOverdueQueryHandler
        : IRequestHandler<GetToDoListByOverdueQuery,
            ServiceResult<GetToDoListByOverdueResponseDto>>
    {
        private readonly IGetToDoListByOverdueOrchestrator _orchestrator;
        private readonly ILogger _logger;

        public GetToDoListByOverdueQueryHandler(
            IGetToDoListByOverdueOrchestrator orchestrator,
            ILogger logger)
        {
            _orchestrator = orchestrator;
            _logger = logger;
        }

        public async Task<ServiceResult<GetToDoListByOverdueResponseDto>> Handle(
            GetToDoListByOverdueQuery request,
            CancellationToken cancellationToken)
        {
            _logger.Information("GetToDoListByOverdue started");

            try
            {
                var result = await _orchestrator.GetListByOverdueAsync(
                    request,
                    cancellationToken);

                _logger.Information("GetToDoListByOverdue completed successfully");

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "GetToDoListByOverdue failed");

                return ServiceResult<GetToDoListByOverdueResponseDto>
                    .Fail(ServiceErrorCode.Unknown);
            }
        }
    }
}