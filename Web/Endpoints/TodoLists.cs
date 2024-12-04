using Application.TodoLists.Command;
using Application.TodoLists.Queries.GetTodos;
using GioWebsite.Web.Infrastructure;
using MediatR;
using Web.Infrastructure;

namespace Web.Endpoints
{
    public class TodoLists : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(this)
                .MapGet(GetTodoLists)
                .MapPost(CreateTodoList);
        }

        public Task<TodosVm> GetTodoLists(ISender sender)
        {
            return sender.Send(new GetTodosQuery());
        }

        public Task<int> CreateTodoList(ISender sender, CreateTodoListCommand command)
        {
            return sender.Send(command);
        }
    }
}
