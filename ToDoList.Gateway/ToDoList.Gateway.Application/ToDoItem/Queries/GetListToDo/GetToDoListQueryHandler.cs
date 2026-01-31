using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using ToDoList.Gateway.Application.ToDoItem.Queries.Containers;
using ToDoList.Gateway.Application.ToDoItem.Queries.CriteriaSplitter;
using ToDoList.Gateway.Application.ToDoItem.Queries.GetByStatus;

namespace ToDoList.Gateway.Application.ToDoItem.Queries.GetListToDo
{
    public class GetToDoListQueryHandler : IRequestHandler<GetToDoListQuery, ToDoListContainer>
    {
        private readonly ITaskStateServiceApiClientAdapter _clientStateServiceAdapter;
        private readonly ITaskManagerApiClientAdapter _clientManagerAdapter;
        public GetToDoListQueryHandler(ITaskStateServiceApiClientAdapter clientStateServiceAdapter)
        {
            _clientStateServiceAdapter = clientStateServiceAdapter;
        }
        public async Task<ToDoListContainer> Handle(GetToDoListQuery request, CancellationToken cancellationToken)
        {
            var query = new CriteriaSplitterQuery()
            {
                UserId = request.UserId
            };

            var resultTaskState = _clientStateServiceAdapter.GetToDoListByStatusAsync(query);

            var resultManager = _clientManagerAdapter.GetToDoListAsync(query);

            await Task.WhenAll(resultTaskState, resultManager);

            return ToDoListContainer.Merge(
                resultTaskState.Result,
                resultManager.Result
            );

        }
    }
}
