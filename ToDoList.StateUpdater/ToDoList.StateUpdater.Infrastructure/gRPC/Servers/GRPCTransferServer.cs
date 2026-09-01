using Grpc.Core;
using Microsoft.Extensions.Options;
using ToDoList.StateUpdater.Application.Services;
using ToDoList.StateUpdater.Contracts;
using ToDoList.StateUpdater.Contracts.Protos.Indexer;
using ToDoList.StateUpdater.Infrastructure.gRPC.Options;
using Serilog;

namespace ToDoList.StateUpdater.Infrastructure.gRPC.Servers
{
    public class GRPCTransferServer : DeadlineTransferService.DeadlineTransferServiceBase
    {
        private readonly GRPCServerOptions _options;
        private readonly DeadlineInMemoryQueue _queue;
        private readonly ILogger _logger;

        public GRPCTransferServer(
            IOptions<GRPCServerOptions> options, 
            DeadlineInMemoryQueue queue,
            ILogger logger)
        {
            _options = options.Value;
            _queue = queue;
            _logger = logger;
        }

        public override async Task<TransferResult> TransferDeadlineBatch(
            DeadlineBatchRequest request, 
            ServerCallContext context)
        {
            if (request.Items.Count == 0)
            {
                _logger.Warning("Message is empty");

                return new TransferResult { IsSuccess = true };
            }
                

            var appKey = context.RequestHeaders.FirstOrDefault(header => 
                header.Key == "x-api-key");

            if (appKey == null || _options.ApplicationKey != appKey.Value)
            {
                _logger.Debug($"Service sent wrong key - {appKey}");

                return new TransferResult { IsSuccess = false };
            }

            try
            {
                foreach (var item in request.Items)
                {
                    var dto = new DeadlineUpdateDto(
                        item.TaskId, 
                        item.UserId, 
                        item.DeadlineUnix, 
                        item.CreatedAtUnix);

                    await _queue.Writer.WriteAsync(dto, context.CancellationToken);
                }

                _logger.Debug("Unpaking success");

                return new TransferResult { IsSuccess = true };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "There were problems while processing message.");

                return new TransferResult { IsSuccess = false };
            }
        }
    }
}
