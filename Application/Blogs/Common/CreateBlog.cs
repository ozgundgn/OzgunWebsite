using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Blogs.Common
{
    public class CreateBlogCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Link { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }

    public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateBlogCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
            var entity = new Blog()
            {
                Content = request.Content,
                Link = request.Link,
                CreatedDate = request.CreatedDate,
                LastUpdatedDate = request.LastUpdatedDate,
                Title = request.Title
            };
            await _context.Blogs.AddAsync(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
