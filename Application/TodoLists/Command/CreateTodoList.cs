using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.TodoLists.Command;

public record CreateTodoListCommand : IRequest<int>
{
    public string? Title { get; init; }
    public string Description { get; set; }
}

public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = new TodoList();

        entity.Title = request.Title;
        entity.Description = request.Description;
        _context.TodoLists.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
