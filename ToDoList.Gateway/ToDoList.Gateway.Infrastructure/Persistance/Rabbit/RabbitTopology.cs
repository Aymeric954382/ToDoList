using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Options;
using RabbitMQ.AMQP.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Contracts.ApiClients.Rabbit;
using ToDoList.Gateway.Contracts.Exceptions;
using ToDoList.Gateway.Infrastructure.Persistance.Rabbit.Options;

namespace ToDoList.Gateway.Infrastructure.Persistance.Rabbit
{
    public class RabbitTopology
    {
        private IManagement _management;

        private readonly RabbitTopologyOptions _topologyOptions;

        private readonly HashSet<RabbitEndpoint> _declaredEndpoints = [];

        public RabbitTopology(
            IConnection connection,
            IOptions<RabbitTopologyOptions> options)
        {
            _management = connection.Management();

            _topologyOptions = options.Value;

        }

        public async Task EnsureTopologyAsync(RabbitEndpoint endpoint)
        {
            if (_declaredEndpoints.Contains(endpoint))
                return;

            await DeclareExchangeAsync(endpoint);
            await DeclareQueueAsync(endpoint);
            await DeclareBindingAsync(endpoint);

            _declaredEndpoints.Add(endpoint);
        }

        private async Task DeclareExchangeAsync(RabbitEndpoint endpoint)
        {
            try
            {
                await _management
                    .Exchange(endpoint.Exchange)
                    .DeclareAsync();
            }
            catch(Exception ex)
            {
                throw new RabbitTopologyException(endpoint, ex);
            }

        }

        private async Task DeclareQueueAsync(RabbitEndpoint endpoint)
        {
            try
            {
                await _management
                    .Queue(endpoint.QueueName)
                    .Type(_topologyOptions.QueueType)
                    .AutoDelete(_topologyOptions.AutoDelete)
                    .MaxLength(_topologyOptions.MaxLength)
                    .DeclareAsync();

            }
            catch (Exception ex)
            {
                throw new RabbitTopologyException(endpoint, ex);
            }

        }

        private async Task DeclareBindingAsync(RabbitEndpoint endpoint)
        {
            try
            {
                await _management
                    .Binding()
                    .SourceExchange(endpoint.Exchange)
                    .DestinationQueue(endpoint.QueueName)
                    .Key(endpoint.RoutingKey)
                    .BindAsync();
            }
            catch (Exception ex)
            {
                throw new RabbitTopologyException(endpoint, ex);
            }
        }
    }
}
