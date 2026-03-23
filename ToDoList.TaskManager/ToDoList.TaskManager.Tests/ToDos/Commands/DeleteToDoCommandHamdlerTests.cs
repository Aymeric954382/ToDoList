using FluentAssertions;
using Moq;
using ToDoList.TaskManager.Application.Features.ToDoItems.Commands.DeleteToDo;
using ToDoList.TaskManager.Application.Interfaces.Repository;
using ToDoList.TaskManager.Domain;

namespace ToDoList.TaskManager.Tests.ToDos.Commands
{
    public class DeleteToDoCommandHandlerTests
    {
        [Fact]
        public async Task Handle_WhenToDoExistsAndUserMatches_Success()
        {
            // Arrange
            var mockRepo = new Mock<IToDoRepository>();

            var userId = Guid.NewGuid();
            var todoId = Guid.NewGuid();

            var existingItem = new ToDoItem
            {
                Id = todoId,
                UserId = userId,
                Title = "Test task",
                Details = "Some details",
            };

            mockRepo
                .Setup(r => r.GetByIdAsync(todoId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingItem);

            ToDoItem deletedEntity = null;

            mockRepo
                .Setup(r => r.DeleteAsync(It.IsAny<ToDoItem>(), It.IsAny<CancellationToken>()))
                .Callback<ToDoItem, CancellationToken>((ent, ct) => deletedEntity = ent)
                .Returns(Task.CompletedTask);

            var handler = new DeleteToDoCommandHandler(mockRepo.Object);

            var command = new DeleteToDoCommand
            {
                Id = todoId,
                UserId = userId
            };

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            deletedEntity.Should().NotBeNull();
            deletedEntity.Id.Should().Be(todoId);
            deletedEntity.UserId.Should().Be(userId);

            mockRepo.Verify(r => r.GetByIdAsync(todoId, It.IsAny<CancellationToken>()), Times.Once);
            mockRepo.Verify(r => r.DeleteAsync(It.IsAny<ToDoItem>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}


