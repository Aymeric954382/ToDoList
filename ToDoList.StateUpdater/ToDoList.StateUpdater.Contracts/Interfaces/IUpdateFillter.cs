using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.StateUpdater.Contracts.Interfaces
{
    public interface IUpdateFillter
    {
        IEnumerable<DeadlineUpdateDto> FilterValidUpdates(IEnumerable<DeadlineUpdateDto> items);
    }

}
