using ToDoList.DeadlineIndexer.Application.CacheService;
using ToDoList.DeadlineIndexer.Application.Interfaces;
using ToDoList.DeadlineIndexer.Domain;
using Serilog;

namespace ToDoList.DeadlineIndexer.Application.TransferService
{
    public class DeadlineProcessor
    {
        private readonly CacheExtractor _cacheExtractor;
        private readonly IDeadlineNotificationClient _client;
        private readonly IDeadlineDbRepository _repository;
        private readonly ILogger _logger;

        public DeadlineProcessor(
            CacheExtractor cacheExtractor,
            IDeadlineNotificationClient client,
            IDeadlineDbRepository repository,
            ILogger logger)
        {
            _cacheExtractor = cacheExtractor;
            _client = client;
            _repository = repository;
            _logger = logger;
        }

        public async Task ProcessPendingDeadlinesAsync(
            CancellationToken cancellationToken)
        {
            var stubs = await _cacheExtractor.ExtructCacheAsync(cancellationToken);

            _logger.Debug("Extracting deadlines in redis");

            if (stubs.Count == 0) return;

            try
            {
                var operationId = Guid.NewGuid();
                
                await _client.SendDeadlinesAsync(stubs, operationId.ToString(), cancellationToken);

                _logger.Debug($"Deadlines sent. Operation Id: {operationId}, Count records: {stubs.Count}");

                await _repository.SaveProcessedDeadlinesAsync(stubs, operationId, cancellationToken);

                _logger.Debug($"Deadlines write to db. Operation Id {operationId}, Count records {stubs.Count}");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error while processing");
            }
        }
    }
}
