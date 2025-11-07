using FluentAssertions;
using Moq;
using ToDoList.Application.Interfaces.Repository;
using ToDoList.Application.ToDoItems.Commands.ChangeToDoDueDate;
using ToDoList.Domain.ToDo;

namespace ToDoList.Tests.ToDos.Commands
{
    public class ChangeToDoDueDateCommandHandlerTests
    {
        [Fact]
        public async Task ChandeToDoDueDate_Success()
        {
            var mockRepo = new Mock<IToDoRepository>();

            var userId = Guid.NewGuid();
            var todoId = Guid.NewGuid();

            var oldDueDate = DateTime.UtcNow.AddDays(3);
            var newDueDate = DateTime.UtcNow.AddDays(10);

            var existingItem = new ToDoItem
            {
                Id = todoId,
                UserId = userId,
                Title = "Old task",
                DueDate = oldDueDate,
            };

            mockRepo
                .Setup(r => r.GetByIdAsync(todoId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingItem);

            ToDoItem updatedEntity = null;

            mockRepo
                .Setup(r => r.UpdateAsync(It.IsAny<ToDoItem>(), It.IsAny<CancellationToken>()))
                .Callback<ToDoItem, CancellationToken>((ent, ct) => updatedEntity = ent)
                .Returns(Task.CompletedTask);

            var handler = new ChangeToDoDueDateCommandHandler(mockRepo.Object);

            var command = new ChangeToDoDueDateCommand
            {
                Id = todoId,
                UserId = userId,
                DueDate = newDueDate
            };

            //Act
            await handler.Handle(command, CancellationToken.None);

            //Assert
            updatedEntity.Should().NotBeNull();
            updatedEntity.DueDate.Should().Be(newDueDate);
            updatedEntity.DueDate.Should().NotBe(oldDueDate);
            updatedEntity.Title.Should().Be("Old task");

            mockRepo.Verify(r => r.GetByIdAsync(todoId, It.IsAny<CancellationToken>()), Times.Once);
            mockRepo.Verify(r => r.UpdateAsync(It.IsAny<ToDoItem>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
