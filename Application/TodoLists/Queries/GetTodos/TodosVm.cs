namespace Application.TodoLists.Queries.GetTodos
{
    public class TodosVm
    {
        public IReadOnlyCollection<TodoListDto> Lists { get; init; } = Array.Empty<TodoListDto>();
    }
}
