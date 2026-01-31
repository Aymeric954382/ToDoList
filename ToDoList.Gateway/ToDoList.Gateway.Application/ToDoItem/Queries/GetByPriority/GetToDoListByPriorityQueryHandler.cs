using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using ToDoList.Gateway.Application.ToDoItem.Queries.Containers;
using ToDoList.Gateway.Application.ToDoItem.Queries.CriteriaSplitter;

namespace ToDoList.Gateway.Application.ToDoItem.Queries.GetByPriority
{
    public class GetToDoListByPriorityQueryHandler : IRequestHandler<GetToDoListByPriorityQuery, ToDoListContainer>
    {
        private readonly ITaskStateServiceApiClientAdapter _clientStateServiceAdapter;
        private readonly ITaskManagerApiClientAdapter _clientManagerAdapter;
        public GetToDoListByPriorityQueryHandler(ITaskStateServiceApiClientAdapter clientStateServiceAdapter)
        {
            _clientStateServiceAdapter = clientStateServiceAdapter;
        }
        public async Task<ToDoListContainer> Handle(GetToDoListByPriorityQuery request, CancellationToken cancellationToken)
        {
            var query = new CriteriaSplitterQuery()
            {
                UserId = request.UserId,
                Priority = request.Priority
            };

            var resultTaskState = _clientStateServiceAdapter.GetToDoListByPriorityAsync(query);

            var resultManager = _clientManagerAdapter.GetToDoListAsync(query);

            await Task.WhenAll(resultTaskState, resultManager);

            return ToDoListContainer.Merge(
                resultTaskState.Result,
                resultManager.Result
            );

        }       
    }
}
