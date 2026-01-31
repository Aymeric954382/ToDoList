

using ToDoList.Gateway.Contracts.ApiClients.ResponseDtos;

namespace ToDoList.Gateway.Application.ToDoItem.Queries.Containers
{
    public class ToDoListContainer
    {
        public List<ToDoItemDto> Items { get; set; } = new();

        public static ToDoListContainer Merge(
            ToDoListContainer first,
            ToDoListContainer second)
        {
            var merged = new ToDoListContainer();

            if (first?.Items != null)
                merged.Items.AddRange(first.Items);

            if (second?.Items != null)
                merged.Items.AddRange(second.Items);

            merged.Items = merged.Items
                .GroupBy(x => x.Id)
                .Select(g => g.First())
                .ToList();

            return merged;
        }
    }
}
