namespace Application.Blogs.Queries.GetBlogs
{
    public class BlogDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Link { get; set; }
        public byte[]? Image { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public List<BlogPartDto> BlogParts { get; set; }
    }
    public class ImageDto
    {
        public int ImageNo { get; set; }
        public string ImageInfo { get; set; }
    }

    public class BlogPartDto
    {
        public int PartNo { get; set; }
        public string Text { get; set; }
        public List<ImageDto> Images { get; set; }
        public List<CodeBlockDto> CodeBlocks { get; set; }

    }

    public class CodeBlockDto
    {
        public int CodeNo { get; set; }
        public string Code { get; set; }
    }
}
