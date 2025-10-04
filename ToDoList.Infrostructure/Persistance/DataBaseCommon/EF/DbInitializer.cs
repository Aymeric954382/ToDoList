using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Infrostructure.Persistance.DataBaseCommon.EF
{
    public class DbInitializer
    {
        public static void Initialize(ToDoItemsDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
