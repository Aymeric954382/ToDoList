using Amqp;
using RabbitMQ.AMQP.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Application.Interfaces.Rabbit;
using ToDoList.Gateway.Contracts.ApiClients.Rabbit;

namespace ToDoList.Gateway.Infrastructure.Persistance.Rabbit
{
    public class RabbitPublisher : IRabbitPublisher
    {
        public RabbitPublisherFactory _publisherFactory;

        public RabbitMessageFactory _messageFactory;

        public RabbitPublisher(
            RabbitPublisherFactory publisherFactory, 
            RabbitMessageFactory messageFactory)
        {
            _publisherFactory = publisherFactory;
            _messageFactory = messageFactory;
        }
        public async Task PublishAsync<T>(
            RabbitEndpoint endpoint, 
            T dto, 
            CancellationToken cancellationToken = default)
        {
            var publisher = _publisherFactory.GetPublisher(endpoint.Exchange);

            var message = _messageFactory.Create(endpoint, dto);

            await publisher.PublishAsync(message, cancellationToken);
        }
    }
}
