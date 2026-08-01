using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Infrastructure.Persistance.Rabbit
{
    public class MessageSerializer
    {
        public byte[] Serialize<T>(T message)
        {
            return JsonSerializer.SerializeToUtf8Bytes(message);
        }

        public T Deserialize<T>(byte[] body)
        {
            return JsonSerializer.Deserialize<T>(body)
                   ?? throw new InvalidOperationException(
                       $"Failed to deserialize {typeof(T).Name}.");
        }
    }
}
