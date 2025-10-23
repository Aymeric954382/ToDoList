using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Identity.Application.Common.Interfaces
{
    public interface IWithResultCommand<T> : IRequest<T>
    {
    }
}
