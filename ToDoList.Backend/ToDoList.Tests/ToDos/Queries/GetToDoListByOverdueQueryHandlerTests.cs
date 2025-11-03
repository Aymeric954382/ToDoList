using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using ToDoList.Application.Interfaces.Repository;
using ToDoList.Application.ToDoItems.Queries.GetOverdueToDos;
using ToDoList.Application.ToDoItems.Queries.ResponseDtos;
using ToDoList.Domain.ToDo;
using ToDoList.Tests.Common;
using ToDoList.Tests.Common.InMemoryRepository;
using Xunit.Abstractions;

namespace ToDoList.Tests.ToDos.Queries
{
    public class GetToDoListByOverdueQueryHandlerTests : TestBase
    {
        [Fact]
        public async Task GetToDoByOverdue_Success()
        {
            //Assets
            var repo = new InMemoryToDoRepository();

            var userId = Guid.NewGuid();
            var creationDate = DateTime.Today;
            var todayDate = creationDate.AddDays(2);
            var dueDate = creationDate.AddDays(1);

            var fakeData = new List<ToDoItem>()
            {
                new ToDoItem() { Id = Guid.NewGuid(), UserId = userId, Title = "Task 1", CreationDate = creationDate, DueDate = dueDate },
                new ToDoItem() { Id = Guid.NewGuid(), UserId = userId, Title = "Task 2", CreationDate = creationDate, DueDate = dueDate.AddDays(3) },
                new ToDoItem() { Id = Guid.NewGuid(), UserId = userId, Title = "Task 3", CreationDate = creationDate, DueDate = dueDate }
            };

            foreach (var item in fakeData)
                await repo.AddAsync(item);

            var handler = new GetToDoListOverdueQueryHandler(repo, Mapper);

            var query = new GetToDoListOverdueQuery()
            {
                UserId = userId
            };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.ToDoItems.Should().HaveCount(2);
            result.ToDoItems.Should().OnlyContain(i => i.DueDate <= todayDate);
        }
    }
}
