namespace Application.Blogs.Queries.GetBlogs
{
    public class BlogsVm
    {
        public IReadOnlyCollection<BlogDto> Lists { get; init; } = Array.Empty<BlogDto>();
    }
}
