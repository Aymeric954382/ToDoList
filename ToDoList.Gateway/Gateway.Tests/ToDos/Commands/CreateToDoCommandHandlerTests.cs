using FluentAssertions;
using Moq;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.CreateToDo;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using ToDoList.Gateway.Contracts.ApiClients.ResponseDtos.ResponseServiceResultsContainer;
using ToDoList.Gateway.Contracts.ApiClients.TaskManagerApiClient.TaskManagerResponseDtos.ResponseDtos.Create;
using ToDoList.Gateway.Contracts.ApiClients.TaskStateServiceApiClient.TaskStateServiceResponseDtos.ResponseDtos.Create;

namespace Gateway.Tests.ToDos.Commands
{
    public class CreateToDoCommandHandlerTests
    {
        [Fact]
        public async Task CreateToDo_Success()
        {
            var mockAdapter_Manager = new Mock<ITaskManagerApiClientAdapter>();

            var mockAdapter_StateService = new Mock<ITaskStateServiceApiClientAdapter>();

            var response_ManagerService = new ServiceResult<TaskStateServiceCreateResponseDto>()
            {
                Data = new TaskStateServiceCreateResponseDto()
                {
                    Id = Guid.NewGuid()
                },
                ExecutionSuccess = true,
                Error = null
            };

            var response_StateService = new ServiceResult()
            {
                ExecutionSuccess = true,
                Error = null
            };

            var command = new CreateToDoCommand
            {
                UserId = Guid.NewGuid(),
                Title = "New Title",
                Details = "New Details"
            };

            mockAdapter_Manager.Setup(r => r.CreateAsync(command, CancellationToken.None))
                .ReturnsAsync(response_ManagerService);

            mockAdapter_StateService.Setup(r => r.CreateAsync(command, response_ManagerService.Data.Id, CancellationToken.None))
                .ReturnsAsync(response_StateService);

            var handler = new CreateToDoCommandHandler(mockAdapter_StateService.Object, mockAdapter_Manager.Object);

            var response = await handler.Handle(command, CancellationToken.None);

            response.Should().NotBe(Guid.Empty);

            mockAdapter_Manager.Verify(r => r.CreateAsync(command, CancellationToken.None), Times.Once);
            mockAdapter_StateService.Verify(r => r.CreateAsync(command, response_ManagerService.Data.Id, 
                CancellationToken.None), Times.Once);
        }
    }
}
