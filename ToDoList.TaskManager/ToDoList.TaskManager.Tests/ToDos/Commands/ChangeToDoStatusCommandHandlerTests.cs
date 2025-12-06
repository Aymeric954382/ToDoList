using FluentAssertions;
using Moq;
using ToDoList.TaskManager.Application.Interfaces.Repository;
using ToDoList.TaskManager.Application.ToDoItems.Commands.ChangeToDoStatus;
using ToDoList.TaskManager.Domain;
using ToDoList.TaskManager.Domain.ValueObjects;

namespace ToDoList.TaskManager.Tests.ToDos.Commands
{
    public class ChangeToDoStatusCommandHandlerTests
    {
        [Fact]
        public async Task ChangeToDoStatus_Success()
        {
            var mockRepo = new Mock<IToDoRepository>();

            var userId = Guid.NewGuid();
            var todoId = Guid.NewGuid();

            var oldStatus = ToDoStatus.Active;
            var newStatus = ToDoStatus.Completed;

            var existingItem = new ToDoItem
            {
                Id = todoId,
                UserId = userId,
                Title = "Old task",
                Status = oldStatus,
            };

            mockRepo
                .Setup(r => r.GetByIdAsync(todoId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingItem);

            ToDoItem updatedEntity = null;

            mockRepo
                .Setup(r => r.UpdateAsync(It.IsAny<ToDoItem>(), It.IsAny<CancellationToken>()))
                .Callback<ToDoItem, CancellationToken>((ent, ct) => updatedEntity = ent)
                .Returns(Task.CompletedTask);

            var handler = new ChangeToDoStatusCommandHandler(mockRepo.Object);

            var command = new ChangeToDoStatusCommand
            {
                Id = todoId,
                UserId = userId,
                Status = newStatus
            };

            await handler.Handle(command, CancellationToken.None);

            updatedEntity.Should().NotBeNull();
            updatedEntity.Status.Should().Be(newStatus);
            updatedEntity.Status.Should().NotBe(oldStatus);
            updatedEntity.Title.Should().Be("Old task");

            mockRepo.Verify(r => r.GetByIdAsync(todoId, It.IsAny<CancellationToken>()), Times.Once);
            mockRepo.Verify(r => r.UpdateAsync(It.IsAny<ToDoItem>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
