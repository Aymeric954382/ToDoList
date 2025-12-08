using FluentAssertions;
using MockQueryable;
using Moq;
using ToDoList.TaskStateService.Application.Interfaces.Repository;
using ToDoList.TaskStateService.Application.ToDoItems.Queries.GetByStatus;
using ToDoList.TaskStateService.Domain;
using ToDoList.TaskStateService.Domain.ValueObjects;
using ToDoList.TaskStateService.Tests.Common;

namespace ToDoList.TaskStateService.Tests.ToDos.Queries
{
    public class GetToDoListByStatusQueryHandlerTests : TestBase
    {
        [Fact]
        public async Task GetToDoListStatus_Success()
        {
            //Assets

            var userId = Guid.NewGuid();
            var status = ToDoStatus.Active;

            var mockRepo = new Mock<IToDoRepository>();

            var fakeData = new List<ToDoItem>
            {
                new ToDoItem { Id = Guid.NewGuid(), UserId = userId, Status = ToDoStatus.Active},
                new ToDoItem { Id = Guid.NewGuid(), UserId = userId, Status = ToDoStatus.Expired},
                new ToDoItem { Id = Guid.NewGuid(), UserId = userId, Status = ToDoStatus.Active}
            };

            var mock = fakeData.BuildMock().AsQueryable();

            mockRepo.Setup(x => x.AsQueryable()).Returns(mock);

            var handler = new GetToDoListByStatusQueryHandler(mockRepo.Object, Mapper);

            var query = new GetToDoListByStatusQuery
            {
                UserId = userId,
                Status = status
            };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.ToDoItems.Should().HaveCount(2);
            result.ToDoItems.Should().OnlyContain(i => i.Status == ToDoStatus.Active);

            mockRepo.Verify(r => r.AsQueryable(), Times.Once);
        }
    }
}
