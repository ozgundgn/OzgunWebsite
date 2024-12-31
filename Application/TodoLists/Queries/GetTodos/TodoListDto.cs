namespace Application.TodoLists.Queries.GetTodos;

public class TodoListDto
{
    public TodoListDto()
    {
        Items = Array.Empty<TodoItemDto>();
    }

    public int Id { get; init; }

    public string? Title { get; init; }

    public string? Description { get; init; }

    public IReadOnlyCollection<TodoItemDto> Items { get; init; }
}
