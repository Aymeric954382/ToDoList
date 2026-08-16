using Grpc.Core;
using Microsoft.Extensions.Options;
using ToDoList.DeadlineIndexer.Application.Interfaces;
using ToDoList.DeadlineIndexer.Contracts.Protos;
using ToDoList.DeadlineIndexer.Domain;

namespace ToDoList.DeadlineIndexer.Infrastructure.Persistance.gRPC
{
    public class GRPCDeadlineNotificationClient : IDeadlineNotificationClient
    {
        private readonly DeadlineTransferService.DeadlineTransferServiceClient _client;
        private readonly GRPCOptions _grpcOptions;
        public GRPCDeadlineNotificationClient(
            DeadlineTransferService.DeadlineTransferServiceClient client,
             IOptions<GRPCOptions> grpcOptions)
        {
            _client = client;
            _grpcOptions = grpcOptions.Value;
        }
        public async Task SendDeadlinesAsync(
            List<DeadlineStub> stubs, 
            CancellationToken cancellationToken)
        {
            var request = new DeadlineBatchRequest();

            foreach (var stub in stubs)
            {
                var model = new DeadlineModel
                {
                    TaskId = stub.TaskId.ToString(),
                    UserId = stub.UserId.ToString(),
                    DeadlineUnix = stub.DeadLineUnix
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
                cancellationToken: cancellationToken);

            if (!response.IsSuccess)
            {
                throw new InvalidOperationException("");
            }
        }
    }
}
