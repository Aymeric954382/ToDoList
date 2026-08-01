using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Application.Interfaces.Rabbit
{
    public interface IMessageSerializer
    {
        byte[] Serialize<T>(T message);

        T Deserialize<T>(byte[] body);
    }
}
