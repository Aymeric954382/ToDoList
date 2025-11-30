using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Worker.Domain.ValueObjects;

namespace ToDoList.Worker.Application.Common.Helper
{
    public static class StatusRules
    {
        public static ToDoStatus GetStatus(double remainingPercent)
        {
            if (remainingPercent > 66) return ToDoStatus.Active;
            if (remainingPercent > 0) return ToDoStatus.ExpiringSoon;
            return ToDoStatus.Expired;
        }
    }

}
