using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.AMQP.Client;
using Microsoft.Extensions.Options;
using RabbitMQ.AMQP.Client.Impl;
using ToDoList.TaskStateService.Infrastructure.Persistance.Rabbit.Options;

namespace ToDoList.TaskStateService.Infrastructure.Persistance.Rabbit
{
    public class RabbitConnection
    {
        private IConnection _connection;

        private IEnvironment _environment;

        private ConnectionSettings _connectionSetting;

        public RabbitConnection(IOptions<RabbitOptions> options)
        {
            RabbitOptions rabbit = options.Value;

            _connectionSetting = ConnectionSettingsBuilder.Create()
                .Host(rabbit.Host)
                .Port(rabbit.Port)
                .Password(rabbit.Password)
                .User(rabbit.UserName)
                .VirtualHost(rabbit.VirtualHost)
                .Build();
        }

        public async Task<IConnection> GetOrCreateConnectionAsync(CancellationToken cancellationToken)
        {
            if (_connection != null)
                return _connection;

            try
            {
                _environment = AmqpEnvironment.Create(_connectionSetting);

                _connection = await _environment.CreateConnectionAsync(cancellationToken);

            }

            catch (Exception ex)
            {
                throw new ConnectionException(
                    "Failed to connect to RabbitMQ.",
                    ex);
            }

            return _connection;
        }
    }
}
