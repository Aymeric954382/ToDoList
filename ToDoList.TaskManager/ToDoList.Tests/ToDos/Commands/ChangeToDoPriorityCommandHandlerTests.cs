using FluentAssertions;
using Moq;
using ToDoList.Application.Interfaces.Repository;
using ToDoList.Application.ToDoItems.Commands.ChangeToDoPriority;
using ToDoList.Domain.ToDo;
using ToDoList.Domain.ToDo.ValueObjects;

namespace ToDoList.Tests.ToDos.Commands
{
    public class ChangeToDoPriorityCommandHandlerTests
    {
        [Fact]
        public async Task ChangeToDoPriority_Success()
        {
            var mockRepo = new Mock<IToDoRepository>();

            var userId = Guid.NewGuid();
            var todoId = Guid.NewGuid();

            var oldPriority = ToDoPriority.Low;
            var newPriority = ToDoPriority.Immediately;

            var existingItem = new ToDoItem
            {
                Id = todoId,
                UserId = userId,
                Title = "Old task",
                Priority = oldPriority,
            };

            mockRepo
                .Setup(r => r.GetByIdAsync(todoId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingItem);

            ToDoItem updatedEntity = null;

            mockRepo
                .Setup(r => r.UpdateAsync(It.IsAny<ToDoItem>(), It.IsAny<CancellationToken>()))
                .Callback<ToDoItem, CancellationToken>((ent, ct) => updatedEntity = ent)
                .Returns(Task.CompletedTask);

            var handler = new ChangeToDoPriorityCommandHandler(mockRepo.Object);

            var command = new ChangeToDoPriorityCommand
            {
                Id = todoId,
                UserId = userId,
                Priority = newPriority
            };

            await handler.Handle(command, CancellationToken.None);

            updatedEntity.Should().NotBeNull();
            updatedEntity.Priority.Should().Be(newPriority);
            updatedEntity.Priority.Should().NotBe(oldPriority);
            updatedEntity.Title.Should().Be("Old task");

            mockRepo.Verify(r => r.GetByIdAsync(todoId, It.IsAny<CancellationToken>()), Times.Once);
            mockRepo.Verify(r => r.UpdateAsync(It.IsAny<ToDoItem>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
