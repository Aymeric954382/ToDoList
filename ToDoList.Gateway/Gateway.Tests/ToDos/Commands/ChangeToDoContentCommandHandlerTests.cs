using Moq;
using ToDoList.Gateway.Application.Interfaces.ContractsClientAdapter;
using FluentAssertions;
using ToDoList.Gateway.Application.Features.ToDoItem.Commands.ChangeToDoContent;
using ToDoList.Gateway.Application.Features.ResponseServiceResultsContainer;

namespace Gateway.Tests.ToDos.Commands
{
    public class ChangeToDoContentCommandHandlerTests
    {
        [Fact]
        public async Task ChangeToDoContent_Success()
        {
            var mockAdapter = new Mock<ITaskManagerApiClientAdapter>();

            var todoId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var response = new ServiceResult()
            {
                ExecutionSuccess = false,
                Error = "Fail to send"
            };

            var command = new ChangeToDoContentCommand
            {
                Id = todoId,
                UserId = userId,
                Title = "New Title",
                Details = "New Details"
            };

            mockAdapter.Setup(r => r.ChangeContentAsync(command, CancellationToken.None))
                .ReturnsAsync(response);

            var handler = new ChangeToDoContentCommandHandler(mockAdapter.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().BeOfType(typeof(ServiceResult));
            result.Should().NotBeNull();
            result.Should().Be(result.ExecutionSuccess == false && result.Error != null);

            mockAdapter.Verify(r => r.ChangeContentAsync(command, CancellationToken.None), Times.Once);
        }
    }
}
