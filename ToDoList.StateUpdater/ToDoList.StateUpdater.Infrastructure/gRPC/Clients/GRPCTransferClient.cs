using Grpc.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using ToDoList.StateUpdater.Application.Common.Interfaces;
using ToDoList.StateUpdater.Contracts;
using ToDoList.StateUpdater.Contracts.Protos.Updater;
using ToDoList.StateUpdater.Domain;
using ToDoList.StateUpdater.Infrastructure.gRPC.Options;

namespace ToDoList.StateUpdater.Infrastructure.gRPC.Clients
{
    public class GRPCTransferClient : IGRPCTransferClient
    {
        private readonly DeadlineTransferService.DeadlineTransferServiceClient _client;
        private readonly GRPCClientOptions _grpcOptions;
        private readonly Serilog.ILogger _logger;
        public GRPCTransferClient(
            DeadlineTransferService.DeadlineTransferServiceClient client,
            IOptions<GRPCClientOptions> options,
            Serilog.ILogger logger)
        {
            _client = client;
            var grpcOption = options.Value;
            _grpcOptions = grpcOption;
            _logger = logger;
        }

        public async Task SendUpdateAsync(
            List<DeadlineUpdateDto> stubs,
            CancellationToken cancellationToken)
        {
            var request = new DeadlineBatchRequest();

            foreach(var items in stubs)
            {
                var model = new DeadlineModel()
                {
                    TaskId = items.TaskId.ToString(),
                    UserId = items.UserId.ToString(),
                    DeadlineUnix = items.DeadlineUnix,
                    CreatedAtUnix = items.CreatedAtUnix,
                    
                };

                request.Items.Add(model);
            }

            var headers = new Metadata
            {
                { "x-api-key", _grpcOptions.ApplicationKey }
            };

            _logger.Debug("Send message to TaskStateService");

            var response = await _client.TransferDeadlineBatchAsync(
                request,
                headers,
                DateTime.UtcNow,
                cancellationToken);


            if (!response.IsSuccess)
            {
                _logger.Warning("Service returned a failuer");
            }
        }
    }
}
