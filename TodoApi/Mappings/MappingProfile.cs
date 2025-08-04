using AutoMapper;
using TodoApi.DTOs;
using TodoApi.Entities;

namespace TodoApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TodoItem, TodoItemDTO>().ReverseMap();
        }
    }
}
