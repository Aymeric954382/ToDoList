using FluentAssertions;
using MockQueryable;
using Moq;
using ToDoList.TaskStateService.Application.Interfaces.Repository;
using ToDoList.TaskStateService.Application.ToDoItems.Queries.GetListToDo;
using ToDoList.TaskStateService.Application.ToDoItems.Queries.ResponseDtos;
using ToDoList.TaskStateService.Domain;
using ToDoList.TaskStateService.Domain.ValueObjects;
using ToDoList.TaskStateService.Tests.Common;

namespace ToDoList.TaskStateService.Tests.ToDos.Queries
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
                    Status = ToDoStatus.Active,
                    CreationDate = DateTime.UtcNow
                },
                new ToDoItem()
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Status = ToDoStatus.Cancelled,
                    Priority = ToDoPriority.Low,
                    CreationDate = DateTime.UtcNow
                },
                new ToDoItem()
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Status = ToDoStatus.Expired,
                    CreationDate = DateTime.UtcNow
                },
                new ToDoItem()
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
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
