using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using ToDoList.StateUpdater.Contracts;

namespace ToDoList.StateUpdater.Application.Common.CustomExtensions
{
    public static class ChannelWriterExtensions
    {
        public static void ReadLimited<T>(this ChannelReader<T> channel, List<T> destination, int sizeLimit)
        {
            while (destination.Count < sizeLimit && channel.TryRead(out var value))
            {
                destination.Add(value);
            }
        }
    }
}
