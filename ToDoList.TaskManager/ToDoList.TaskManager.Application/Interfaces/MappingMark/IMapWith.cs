using AutoMapper;

namespace ToDoList.TaskManager.Application.Interfaces.MappingMark
{
    public interface IMapWith<T>
    {
        public void Mapping(Profile profile) =>
             profile.CreateMap(typeof(T), GetType());
    }
}
