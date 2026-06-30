namespace ToDoList.Gateway.Contracts.ApiClients
{
    public class ServiceApiResponse<T>
    {
        public string Message { get; set; } = default!;
        public T? Data { get; set; }
    }
}
