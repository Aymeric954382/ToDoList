using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.StateUpdater.Contracts
{
    public record DeadlineUpdateDto(
        string TaskId,
        string UserId,
        long DeadlineUnix,
        long CreatedAtUnix
    );
}
