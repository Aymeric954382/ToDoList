using RabbitMQ.AMQP.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.RabbitEndpoints;

namespace ToDoList.Gateway.Infrastructure.Persistance.Rabbit
{
    public class RabbitPublisherFactory
    {
        private readonly RabbitConnection _connection;

        private readonly Dictionary<string, IPublisher> _publishers = [];

        public RabbitPublisherFactory(RabbitConnection connection)
        {
            _connection = connection;
        }

        public async Task BuildPublisherAsync(Assembly assembly, CancellationToken cancellationToken)
        {
            var connection = await _connection.GetOrCreateConnectionAsync(cancellationToken);

            var exchanges = RabbitPublisherProvider
                .GetRegisteredEndpointsFromAssembly(assembly)
                .Select(i => i.Exchange)
                .Distinct()
                .ToList();

            foreach (var exchange in exchanges)
            {
                var publisher = await connection
                    .PublisherBuilder()
                    .Exchange(exchange)
                    .BuildAsync();

                _publishers.Add(exchange, publisher);
            }
            
        }
        public IPublisher GetPublisher(string exchange)
        {
            if (!_publishers.TryGetValue(exchange, out var publisher))
                throw new InvalidOperationException(
                    $"Publisher for exchange '{exchange}' was not initialized.");

            return publisher;
        }
    }
}
