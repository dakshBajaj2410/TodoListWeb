
using AutoMapper;

namespace TodoListWeb.API.Profiles
{
    public class TodoListProfile : Profile
    {
        public TodoListProfile()
        {
            CreateMap<Models.Domain.TodoItems, Models.DTO.TodoItems>()
                .ReverseMap();
        }
    }
}
