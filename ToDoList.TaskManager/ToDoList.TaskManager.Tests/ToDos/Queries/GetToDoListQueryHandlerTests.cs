using AutoMapper;
using FluentAssertions;
using MockQueryable;
using MockQueryable.Moq;
using Moq;
using ToDoList.TaskManager.Application.Features.ToDoItems.Queries.GetListToDo;
using ToDoList.TaskManager.Application.Interfaces.Repository;
using ToDoList.TaskManager.Domain;
using ToDoList.TaskManager.Tests.Common;

namespace ToDoList.TaskManager.Tests.ToDos.Queries
{
    public class GetToDoListQueryHandlerTests : TestBase
    {
        [Fact]
        public async Task GetToDoListQuery_Success()
        {
            //Assets
            var userId = Guid.NewGuid();

            var mockRepo = new Mock<IToDoRepository>();

            var assembly = typeof(GetToDoListResponseDto).Assembly;

            var fakeData = new List<ToDoItem>()
            {
                new ToDoItem()
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Title = "Task 1",
                    Details = "Task 1",
                },
                new ToDoItem()
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Title = "Task 2",
                    Details = "Task 2",
                },
                new ToDoItem()
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Title = "Task 3",
                    Details = "Task 3",
                },
                new ToDoItem()
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Title = "Task 4",
                    Details = "Task 4",
                }
            };

            mockRepo
                .Setup(x => x.GetListByUserIdAsync(userId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(fakeData);

            var handler = new GetToDoListQueryHandler(mockRepo.Object, Mapper);

            var query = new GetToDoListQuery { UserId = userId };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Data.Items.Should().HaveCount(4);

            result.ExecutionSuccess.Should().BeTrue();
            result.Error.Should().BeNull();

            mockRepo.Verify(
                r => r.GetListByUserIdAsync(userId, It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
