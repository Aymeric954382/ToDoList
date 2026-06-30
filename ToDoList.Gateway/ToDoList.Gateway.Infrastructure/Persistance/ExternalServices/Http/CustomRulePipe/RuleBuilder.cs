using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Infrastructure.Persistance.ExternalServices.Http.CustomRulePipe
{
    public class RuleBuilder<T, TValue>
    {
        private readonly RuleContext<T, TValue> _ctx;
        public RuleBuilder(RuleContext<T, TValue> ctx)
        {
            _ctx = ctx;
        }

        public RuleBuilder<T, TValue> NotNull()
        {
            if (_ctx.Value == null)
                _ctx.AddError("Value is null");

            return this;
        }
        public RuleBuilder<T, TValue> NotEmpty()
        {
            if (_ctx.Value is string value && string.IsNullOrEmpty(value))
                _ctx.AddError("Value is empty");

            return this;
        }
        public RuleBuilder<T, TValue> NotEqual(TValue expected)
        {
            if (EqualityComparer<TValue>.Default.Equals(_ctx.Value, expected))
            {
                _ctx.AddError("Non-compliance with the condition {NotEqual}");
            }

            return this;
        }
        public RuleBuilder<T, TValue> Equal(TValue expected)
        {
            if (!EqualityComparer<TValue>.Default.Equals(_ctx.Value, expected))
            {
                _ctx.AddError("Non - compliance with the condition {Equal}");
            }

            return this;
        }
        public void ThrowIfInvalid(string? operation = null, string? service = null)
        {
            if (_ctx.exceptions.Count == 0)
                return;

            var message = new StringBuilder();

            message.AppendLine("Validation failed");

            if (!string.IsNullOrWhiteSpace(operation))
                message.AppendLine($"Operation: {operation}");

            if (!string.IsNullOrWhiteSpace(service))
                message.AppendLine($"Service: {service}");

            foreach (var error in _ctx.exceptions)
            {
                message.AppendLine($"- {error}");
            }

            throw new Exception(message.ToString());
        }
    }
}
