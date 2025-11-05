using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Application.Interfaces.Repository;
using ToDoList.Application.ToDoItems.Commands.ChangeToDoContent;
using ToDoList.Domain.ToDo;

namespace ToDoList.Tests.ToDos.Commands
{
    public class ChangeToDoContentCommandHandlerTests
    {
        [Fact]
        public async Task ChangeToDoContent_Success()
        {
            // Arrange
            var mockRepo = new Mock<IToDoRepository>();

            var todoId = Guid.NewGuid();
            var userId = Guid.NewGuid();

            var existingItem = new ToDoItem
            {
                Id = todoId,
                UserId = userId,
                Title = "Old Title",
                Details = "Old Details"
            };

            mockRepo.Setup(r => r.GetByIdAsync(todoId, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(existingItem);

            mockRepo.Setup(r => r.UpdateAsync(It.IsAny<ToDoItem>(), It.IsAny<CancellationToken>()))
                    .Returns(Task.CompletedTask);

            var handler = new ChangeToDoContentCommandHandler(mockRepo.Object);

            var command = new ChangeToDoContentCommand
            {
                Id = todoId,
                UserId = userId,
                Title = "New Title",
                Details = "New Details"
            };

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            existingItem.Title.Should().Be(command.Title);
            existingItem.Details.Should().Be(command.Details);

            mockRepo.Verify(r => r.GetByIdAsync(todoId, It.IsAny<CancellationToken>()), Times.Once);
            mockRepo.Verify(r => r.UpdateAsync(It.IsAny<ToDoItem>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
