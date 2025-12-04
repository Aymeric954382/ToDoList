using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.TaskStateService.Domain;

namespace ToDoList.TaskStateService.Application.Common.Helper
{
    public static class StatusCalc
    {
        public static double Calc(ToDoItem toDo)
        {
            if (toDo.DueDate is null)
                return 0;

            DateTime start = toDo.CreationDate;
            DateTime end = toDo.DueDate.Value;
            DateTime now = DateTime.UtcNow;

            double totalDuration = (end - start).TotalMinutes;
            double remainingDuration = (end - now).TotalMinutes;

            if (totalDuration <= 0)
                return 0;

            double remainingPercent = remainingDuration / totalDuration * 100;

            return Math.Clamp(remainingPercent, 0, 100);
        }
    }
}
