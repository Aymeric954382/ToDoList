using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Application.Interfaces.Repository;
using ToDoList.Application.ToDoItems.Queries.GetByPriority;
using ToDoList.Application.ToDoItems.Queries.ResponseDtos;
using ToDoList.Domain.ToDo;
using ToDoList.Domain.ToDo.ValueObjects;
using ToDoList.Tests.Common;
using ToDoList.Tests.Common.InMemoryRepository;

namespace ToDoList.Tests.ToDos.Queries
{
    public class GetToDoListByPriorityQueryHandlerTests : TestBase
    {
        [Fact]
        public async Task GetToDoByPriority_Success()
        {
            //Assets
            var repo = new InMemoryToDoRepository();

            var userId = Guid.NewGuid();
            var priority = ToDoPriority.High;

            var mockRepo = new Mock<IToDoRepository>();

            var fakeData = new List<ToDoItem>
            {
                new() { Id = Guid.NewGuid(), UserId = userId, Title = "Task 1", Priority = ToDoPriority.High },
                new() { Id = Guid.NewGuid(), UserId = userId, Title = "Task 2", Priority = ToDoPriority.Low },
                new() { Id = Guid.NewGuid(), UserId = userId, Title = "Task 3", Priority = ToDoPriority.High }
            }.AsQueryable();

            foreach (var item in fakeData)
                await repo.AddAsync(item);

            var handler = new GetToDoListByPriorityQueryHandler(Mapper, repo);

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
