using DnsClient.Internal;
using Grpc.Core;
using Microsoft.Extensions.Options;
using ToDoList.DeadlineIndexer.Application.Interfaces;
using ToDoList.DeadlineIndexer.Contracts.Protos;
using ToDoList.DeadlineIndexer.Domain;
using Serilog;

namespace ToDoList.DeadlineIndexer.Infrastructure.Persistance.gRPC
{
    public class GRPCTransferClient : IDeadlineNotificationClient
    {
        private readonly DeadlineTransferService.DeadlineTransferServiceClient _client;
        private readonly GRPCOptions _grpcOptions;
        private readonly Serilog.ILogger _logger;
        public GRPCTransferClient(
            DeadlineTransferService.DeadlineTransferServiceClient client,
             IOptions<GRPCOptions> grpcOptions,
             Serilog.ILogger logger)
        {
            _client = client;
            _grpcOptions = grpcOptions.Value;
        }
        public async Task SendDeadlinesAsync(
            List<DeadlineStub> stubs,
            string operationId,
            CancellationToken cancellationToken)
        {
            var request = new DeadlineBatchRequest();

            foreach (var stub in stubs)
            {
                var model = new DeadlineModel
                {  
                    TaskId = stub.TaskId.ToString(),
                    UserId = stub.UserId.ToString(),
                    DeadlineUnix = stub.DeadLineUnix,
                    CreatedAtUnix = stub.CreatedAtUnix,
                    OperationId = operationId.ToString()
                };

                request.Items.Add(model);
            }

            var headers = new Metadata
            {
                { "x-api-key", _grpcOptions.ApplicationKey }
            };

            var response = await _client.TransferDeadlineBatchAsync(
                request,
                headers,
                DateTime.UtcNow,
                cancellationToken: cancellationToken);

            _logger.Information("Sent deadlines to Updater");

            if (!response.IsSuccess)
            {
                _logger.Warning("The service returned a failure.");

                throw new InvalidOperationException("The service returned a failure.");
            }
        }
    }
}
