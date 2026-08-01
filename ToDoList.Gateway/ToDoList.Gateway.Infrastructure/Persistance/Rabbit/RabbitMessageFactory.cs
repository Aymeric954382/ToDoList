using Amqp;
using RabbitMQ.AMQP.Client;
using RabbitMQ.AMQP.Client.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Contracts.ApiClients.Rabbit;

namespace ToDoList.Gateway.Infrastructure.Persistance.Rabbit
{
    public class RabbitMessageFactory
    {
        private readonly MessageSerializer _serializer;

        public IMessage Create<T>(
            RabbitEndpoint endpoint,
            T dto)
        {
            var payload = _serializer.Serialize(dto);

            var message = new AmqpMessage(payload);

            message
                .ToAddress()
                .Exchange(endpoint.Exchange)
                .Key(endpoint.RoutingKey)
                .Build();

            return message;
        }
    }
}
