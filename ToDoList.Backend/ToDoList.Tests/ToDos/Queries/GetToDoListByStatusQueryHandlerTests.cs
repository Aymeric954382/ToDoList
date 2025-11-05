using AutoMapper;
using FluentAssertions;
using MockQueryable;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Application.Interfaces.Repository;
using ToDoList.Application.ToDoItems.Queries.GetByPriority;
using ToDoList.Application.ToDoItems.Queries.GetByStatus;
using ToDoList.Application.ToDoItems.Queries.ResponseDtos;
using ToDoList.Domain.ToDo;
using ToDoList.Domain.ToDo.ValueObjects;
using ToDoList.Tests.Common;


namespace ToDoList.Tests.ToDos.Queries
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
                new ToDoItem { Id = Guid.NewGuid(), UserId = userId, Title = "Task 1", Status = ToDoStatus.Active},
                new ToDoItem { Id = Guid.NewGuid(), UserId = userId, Title = "Task 2", Status = ToDoStatus.Expired},
                new ToDoItem { Id = Guid.NewGuid(), UserId = userId, Title = "Task 3", Status = ToDoStatus.Active}
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
