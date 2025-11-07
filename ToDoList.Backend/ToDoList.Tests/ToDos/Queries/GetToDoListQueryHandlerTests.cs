using FluentAssertions;
using MockQueryable;
using Moq;
using ToDoList.Application.Interfaces.Repository;
using ToDoList.Application.ToDoItems.Queries.GetListToDo;
using ToDoList.Application.ToDoItems.Queries.ResponseDtos;
using ToDoList.Domain.ToDo;
using ToDoList.Domain.ToDo.ValueObjects;
using ToDoList.Tests.Common;

namespace ToDoList.Tests.ToDos.Queries
{
    public class GetToDoListQueryHandlerTests : TestBase
    {
        [Fact]
        public async Task GetToDoListQuery_Success()
        {
            //Assets
            var userId = Guid.NewGuid();

            var mockRepo = new Mock<IToDoRepository>();

            var assembly = typeof(ToDoResponseDto).Assembly;

            var fakeData = new List<ToDoItem>()
            {
                new ToDoItem()
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Title = "Task 1",
                    Details = "Task 1",
                    Status = ToDoStatus.Active,
                    CreationDate = DateTime.UtcNow
                },
                new ToDoItem()
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Title = "Task 2",
                    Details = "Task 2",
                    Status = ToDoStatus.Cancelled,
                    Priority = ToDoPriority.Low,
                    CreationDate = DateTime.UtcNow
                },
                new ToDoItem()
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Title = "Task 3",
                    Details = "Task 3",
                    Status = ToDoStatus.Expired,
                    CreationDate = DateTime.UtcNow
                },
                new ToDoItem()
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Title = "Task 4",
                    Details = "Task 4",
                    Status = ToDoStatus.ExpiringSoon,
                    Priority = ToDoPriority.Immediately,
                    CreationDate = DateTime.UtcNow
                }
            };

            var mock = fakeData.BuildMock().AsQueryable();

            mockRepo.Setup(x => x.AsQueryable()).Returns(mock);

            var handler = new GetToDoListQueryHandler(mockRepo.Object, Mapper);

            var query = new GetToDoListQuery { UserId = userId };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.ToDoItems.Should().HaveCount(4);

            mockRepo.Verify(r => r.AsQueryable(), Times.Once);
        }
    }
}
