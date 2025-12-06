
using FluentAssertions;
using MockQueryable;
using Moq;
using ToDoList.TaskManager.Application.Interfaces.Repository;
using ToDoList.TaskManager.Application.ToDoItems.Queries.GetByPriority;
using ToDoList.TaskManager.Domain;
using ToDoList.TaskManager.Domain.ValueObjects;
using ToDoList.TaskManager.Tests.Common;

namespace ToDoList.TaskManager.Tests.ToDos.Queries
{
    public class GetToDoListByPriorityQueryHandlerTests : TestBase
    {
        [Fact]
        public async Task GetToDoByPriority_Success()
        {
            //Assets
            var repo = new Mock<IToDoRepository>();

            var userId = Guid.NewGuid();
            var priority = ToDoPriority.High;

            var mockRepo = new Mock<IToDoRepository>();

            var fakeData = new List<ToDoItem>
            {
                new() { Id = Guid.NewGuid(), UserId = userId, Title = "Task 1", Priority = ToDoPriority.High },
                new() { Id = Guid.NewGuid(), UserId = userId, Title = "Task 2", Priority = ToDoPriority.Low },
                new() { Id = Guid.NewGuid(), UserId = userId, Title = "Task 3", Priority = ToDoPriority.High }
            };

            var mock = fakeData.BuildMock().AsQueryable();

            mockRepo.Setup(x => x.AsQueryable()).Returns(mock);

            var handler = new GetToDoListByPriorityQueryHandler(mockRepo.Object, Mapper);

            var query = new GetToDoListByPriorityQuery
            {
                UserId = userId,
                Priority = priority
            };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.ToDoItems.Should().HaveCount(2);
            result.ToDoItems.Should().OnlyContain(i => i.Priority == ToDoPriority.High);

            mockRepo.Verify(r => r.AsQueryable(), Times.Once);
        }
    }
}
