using Application.Common.Interfaces;

namespace Application.TodoLists.Queries.GetTodos
{
    public record GetTodosQuery : IRequest<TodosVm>;
    public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, TodosVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTodosQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TodosVm> Handle(GetTodosQuery request, CancellationToken cancellationToken)
        {
            var a = new TodosVm
            {
                Lists = await _context.TodoLists.AsNoTracking()
                .ProjectTo<TodoListDto>(_mapper.ConfigurationProvider)
                .OrderBy(x => x.Title)
                .ToListAsync()
            };
            return a;
        }
    }
}
