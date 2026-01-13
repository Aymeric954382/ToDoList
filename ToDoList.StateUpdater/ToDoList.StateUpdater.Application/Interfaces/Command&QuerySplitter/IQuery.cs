using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Application.Interfaces.Command_QuerySplitter
{
    public interface IQuery<T> : IRequest<T> { }
}
