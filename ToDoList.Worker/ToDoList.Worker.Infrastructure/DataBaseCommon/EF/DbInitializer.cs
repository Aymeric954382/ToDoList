using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Worker.Infrastructure.DataBaseCommon.EF
{
    public class DbInitializer
    {
        public static void Initialize(ToDoDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
