using AutoMapper;

namespace ToDoList.Application.Interfaces.MappingMark
{
    public interface IMapWith<T>
    {
        public void Mapping(Profile profile) =>
             profile.CreateMap(typeof(T), GetType());
    }
}
