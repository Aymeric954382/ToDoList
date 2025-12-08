
using FluentAssertions;
using MockQueryable;
using Moq;
using ToDoList.TaskStateService.Application.Interfaces.Repository;
using ToDoList.TaskStateService.Application.ToDoItems.Queries.GetByPriority;
using ToDoList.TaskStateService.Domain;
using ToDoList.TaskStateService.Domain.ValueObjects;
using ToDoList.TaskStateService.Tests.Common;

namespace ToDoList.TaskStateService.Tests.ToDos.Queries
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
                new() { Id = Guid.NewGuid(), UserId = userId, Priority = ToDoPriority.High },
                new() { Id = Guid.NewGuid(), UserId = userId, Priority = ToDoPriority.Low },
                new() { Id = Guid.NewGuid(), UserId = userId, Priority = ToDoPriority.High }
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
