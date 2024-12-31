using Application.Blogs.Common;
using Application.Blogs.Queries.GetBlogs;
using GioWebsite.Web.Infrastructure;
using MediatR;
using Web.Infrastructure;

namespace GioWebsite.Web.Endpoints
{
    public class Blogs : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(this)
                 .MapGet(GetBlogs)
                 .MapPost(CreateBlog);
        }

        public Task<BlogsVm> GetBlogs(ISender sender)
        {
            return sender.Send(new GetBlogsQuery());
        }
        public Task<int> CreateBlog(ISender sender, CreateBlogCommand command)
        {
            return sender.Send(command);
        }
    }
}


