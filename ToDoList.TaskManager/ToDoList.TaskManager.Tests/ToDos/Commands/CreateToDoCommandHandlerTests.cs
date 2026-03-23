using FluentAssertions;
using Moq;
using ToDoList.TaskManager.Application.Common.Exceptions.ServiceErrorCodeToResponse;
using ToDoList.TaskManager.Application.Features.ToDoItems.Commands.CreateToDo;
using ToDoList.TaskManager.Application.Interfaces.Repository;
using ToDoList.TaskManager.Domain;

namespace ToDoList.TaskManager.Tests.ToDos.Commands
{
    public class CreateToDoCommandHandlerTests
    {
        [Fact]
        public async Task CreateToDoCommandHandler_Success()
        {
            // Arrange
            var mockRepo = new Mock<IToDoRepository>();

            ToDoItem savedEntity = null;

            mockRepo
                .Setup(r => r.AddAsync(It.IsAny<ToDoItem>(), It.IsAny<CancellationToken>()))
                .Callback<ToDoItem, CancellationToken>((ent, ct) => savedEntity = ent)
                .Returns(Task.CompletedTask);

            var handler = new CreateToDoCommandHandler(mockRepo.Object);

            var command = new CreateToDoCommand
            {
                UserId = Guid.NewGuid(),
                Title = "Test title",
                Details = "Test details",
            };

            // Act
            var resultId = await handler.Handle(command, CancellationToken.None);

            // Assert
            resultId.Should().NotBe(Guid.Empty);

            mockRepo.Verify(r => r.AddAsync(It.IsAny<ToDoItem>(), It.IsAny<CancellationToken>()), Times.Once);

            savedEntity.Should().NotBeNull();
            savedEntity.Title.Should().Be(command.Title);
            savedEntity.Details.Should().Be(command.Details);
            savedEntity.UserId.Should().Be(command.UserId);
        }

        [Fact]
        public async Task Handle_WhenRepositoryThrows_ShouldBubbleUpException()
        {
            // Arrange
            var mockRepo = new Mock<IToDoRepository>();
            mockRepo
                .Setup(r => r.AddAsync(It.IsAny<ToDoItem>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("DB failed"));

            var handler = new CreateToDoCommandHandler(mockRepo.Object);

            var command = new CreateToDoCommand
            {
                UserId = Guid.NewGuid(),
                Title = "T",
                Details = "D",
                DueDate = DateTime.UtcNow.AddDays(1),
            };

            // Act & Assert
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.False(result.ExecutionSuccess);
            Assert.Equal(ServiceErrorCode.Unknown, result.Error);

            mockRepo.Verify(r => r.AddAsync(It.IsAny<ToDoItem>(), It.IsAny<CancellationToken>()), Times.Once);
        }

    }
}
