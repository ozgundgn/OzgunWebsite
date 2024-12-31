using Application.Common.Interfaces;

namespace Application.Blogs.Queries.GetBlogs
{
    public record GetBlogsQuery : IRequest<BlogsVm> { }
    public class GetBlogsQueryHandler : IRequestHandler<GetBlogsQuery, BlogsVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetBlogsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BlogsVm> Handle(GetBlogsQuery request, CancellationToken cancellationToken)
        {
            var list = await _context.Blogs.AsNoTracking().ProjectTo<BlogDto>(_mapper.ConfigurationProvider).ToListAsync();
            var result = new BlogsVm()
            {
                Lists = list
            };
            return result;
        }
    }
}

