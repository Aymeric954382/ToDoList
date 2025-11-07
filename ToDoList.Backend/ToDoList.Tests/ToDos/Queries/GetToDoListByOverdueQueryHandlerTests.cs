using FluentAssertions;
using MockQueryable;
using Moq;
using ToDoList.Application.Interfaces.Repository;
using ToDoList.Application.ToDoItems.Queries.GetOverdueToDos;
using ToDoList.Domain.ToDo;
using ToDoList.Tests.Common;

namespace ToDoList.Tests.ToDos.Queries
{
    public class GetToDoListByOverdueQueryHandlerTests : TestBase
    {
        [Fact]
        public async Task GetToDoByOverdue_Success()
        {
            //Assets
            var mockRepo = new Mock<IToDoRepository>();

            var userId = Guid.NewGuid();
            var date = DateTime.UtcNow;

            var fakeData = new List<ToDoItem>()
            {
                new ToDoItem() { Id = Guid.NewGuid(), UserId = userId, Title = "Task 1", CreationDate = date.AddDays(-5), DueDate = date.AddDays(-2) },
                new ToDoItem() { Id = Guid.NewGuid(), UserId = userId, Title = "Task 2", CreationDate = date.AddDays(-3), DueDate = date.AddDays(-1) },
                new ToDoItem() { Id = Guid.NewGuid(), UserId = userId, Title = "Task 3", CreationDate = date, DueDate = date.AddDays(+3) }
            };

            var mock = fakeData.BuildMock().AsQueryable();

            mockRepo.Setup(x => x.AsQueryable()).Returns(mock);

            var handler = new GetToDoListOverdueQueryHandler(mockRepo.Object, Mapper);

            var query = new GetToDoListOverdueQuery()
            {
                UserId = userId
            };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.ToDoItems.Should().HaveCount(2);
            result.ToDoItems.Should().OnlyContain(i => i.DueDate <= DateTime.UtcNow);
        }
    }
}
