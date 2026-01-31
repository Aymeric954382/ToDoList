using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Gateway.Application.Interfaces.MappingMark
{
    public interface IMapWithTwo<T1, T2>
    {
        void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(T1), GetType());
            profile.CreateMap(typeof(T2), GetType());
        }
    }
}
