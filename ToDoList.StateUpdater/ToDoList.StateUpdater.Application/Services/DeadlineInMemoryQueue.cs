using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using ToDoList.StateUpdater.Contracts;


namespace ToDoList.StateUpdater.Application.Services
{
    public class DeadlineInMemoryQueue
    {
        private readonly Channel<DeadlineUpdateDto> _channel = Channel.CreateUnbounded<DeadlineUpdateDto>();

        public ChannelWriter<DeadlineUpdateDto> Writer => _channel.Writer;
        public ChannelReader<DeadlineUpdateDto> Reader => _channel.Reader;
    }
}