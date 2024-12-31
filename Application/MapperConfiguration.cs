using Application.Blogs.Queries.GetBlogs;
using Application.TodoLists.Queries.GetTodos;
using Domain.Entities;

namespace Application
{
    public class MapperConfiguration : Profile
    {
        public MapperConfiguration()
        {
            CreateMap<BlogsVm, Blog>().ReverseMap();
            CreateMap<BlogDto, Blog>().ReverseMap();
            CreateMap<BlogPartDto, BlogPart>().ReverseMap();
            CreateMap<ImageDto, Image>().ReverseMap();
            CreateMap<CodeBlockDto, CodeBlock>().ReverseMap();

            CreateMap<TodoList, TodoListDto>().ReverseMap();
            CreateMap<TodoItem, TodoItemDto>().ReverseMap();
        }
    }
}
