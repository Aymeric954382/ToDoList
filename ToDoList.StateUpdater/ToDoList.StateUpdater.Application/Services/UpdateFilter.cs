using ToDoList.StateUpdater.Contracts;
using ToDoList.StateUpdater.Contracts.Interfaces;

namespace ToDoList.StateUpdater.Application.Services
{
    public class UpdateFilter : IUpdateFillter
    {
        private readonly object _lockObject = new();
        private readonly Dictionary<string, long> _processedTasks = new();

        public IEnumerable<DeadlineUpdateDto> FilterValidUpdates(IEnumerable<DeadlineUpdateDto> items)
        {
            var filteredItems = new List<DeadlineUpdateDto>();

            lock (_lockObject)
            {
                foreach (var item in items)
                {
                    if (_processedTasks.TryGetValue(item.TaskId, out var lastCreatedAt))
                    {
                        if (item.CreatedAtUnix <= lastCreatedAt)
                        {
                            continue;
                        }
                    }

                    filteredItems.Add(item);
                    _processedTasks[item.TaskId] = item.CreatedAtUnix;
                }
            }

            return filteredItems;
        }
    }

}
