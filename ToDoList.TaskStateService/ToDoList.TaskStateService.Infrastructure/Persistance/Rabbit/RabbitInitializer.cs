using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.TaskStateService.Infrastructure.Persistance.Helpers;

namespace ToDoList.TaskStateService.Infrastructure.Persistance.Rabbit
{
    public class RabbitInitializer
    {
        private readonly RabbitOperationCatalog _operationCatalog;
        private readonly RabbitConnection _connection;
        private readonly RabbitConsumerFactory _consumerFactory;
        private List<RabbitConsumer> _consumers = [];

        public RabbitInitializer(
            RabbitOperationCatalog operationCatalog,
            RabbitConnection connection,
            RabbitConsumerFactory consumerFactory)
        {
            _operationCatalog = operationCatalog;
            _connection = connection;
            _consumerFactory = consumerFactory;
        }

        public async Task<List<RabbitConsumer>> InitializeAsync(CancellationToken cancellationToken)
        {
            var assembly = typeof(InfrastructureAssemblyMarker).Assembly;

            await _connection.GetOrCreateConnectionAsync(cancellationToken);

            _operationCatalog.RegisterOperations(assembly);

            _consumers = await _consumerFactory.BuildConsumerAsync(assembly, cancellationToken);

            return _consumers;
        }

        public List<RabbitConsumer> GetInitializedConsumers()
        {
            if (_consumers.Count == 0)
                throw new InvalidOperationException("Consumers was not initialized");

            return _consumers;
        }
    }
}
