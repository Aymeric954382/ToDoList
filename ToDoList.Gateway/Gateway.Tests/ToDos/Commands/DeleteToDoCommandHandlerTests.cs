using Moq;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.DeleteToDo;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using ToDoList.Gateway.Contracts.ApiClients.ResponseDtos.ResponseServiceResultsContainer;

namespace Gateway.Tests.ToDos.Commands
{
    public class DeleteToDoCommandHandlerTests
    {
        public async Task DeleteToDo_Success()
        {
            var mockAdapter_Manager = new Mock<ITaskManagerApiClientAdapter>();

            var mockAdapter_StateService = new Mock<ITaskStateServiceApiClientAdapter>();

            Guid taskId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            var response_ManagerService = new ServiceResult()
            {
                ExecutionSuccess = true,
                Error = null
            };

            var response_StateService = new ServiceResult()
            {
                ExecutionSuccess = true,
                Error = null
            };

            var command = new DeleteToDoCommand()
            {
                Id = taskId,
                UserId = userId
            };

            mockAdapter_Manager.Setup(r => r.DeleteAsync(command, CancellationToken.None))
                .ReturnsAsync(response_ManagerService);
            mockAdapter_StateService.Setup(r => r.DeleteAsync(command, CancellationToken.None))
                .ReturnsAsync(response_StateService);

            var handler = new DeleteToDoCommandHandler(mockAdapter_StateService.Object,
                    mockAdapter_Manager.Object);

            var result = handler.Handle(command, CancellationToken.None);

        }
    }
}
