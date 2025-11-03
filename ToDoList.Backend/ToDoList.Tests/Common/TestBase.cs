using AutoMapper;
using Microsoft.Extensions.Logging;
using ToDoList.Application.Common.Mappings.Profiles;
using ToDoList.Application.ToDoItems.Queries.ResponseDtos;

namespace ToDoList.Tests.Common
{
    public abstract class TestBase
    {
        protected readonly IMapper Mapper;
        public TestBase()
        {
            var assembly = typeof(ToDoResponseDto).Assembly;

            var loggerFactory = LoggerFactory.Create(builder => { }); 

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(assembly));
            }, loggerFactory);

            configuration.AssertConfigurationIsValid();

            Mapper = configuration.CreateMapper();
        }
    }
}
