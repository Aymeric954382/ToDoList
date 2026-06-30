namespace ToDoList.Gateway.Infrastructure.Persistance.ExternalServices.Http.CustomRulePipe
{
    public static class Rule
    {
        public static RuleBuilder<T, TValue> MakeFor<T, TValue>(
            T source,
            Func<T, TValue> selector)
        {
            var value = selector(source);

            return new RuleBuilder<T, TValue>(
                new RuleContext<T, TValue>(source, value));
        }
    }
}

