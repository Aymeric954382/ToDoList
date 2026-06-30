using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ToDoList.Gateway.Infrastructure.Persistance.ExternalServices.Http.CustomRulePipe
{
    public class RuleContext<T, TValue>
    {
        public T Instance { get; }
        public TValue Value { get; set; }
        public List<string> exceptions { get; } = new();
        public RuleContext(T instance, TValue value)
        {
            Instance = instance;
            Value = value;
        }

        public void AddError(string error)
        {
            if (!string.IsNullOrWhiteSpace(error))
                exceptions.Add(error);
        }
    }
}
