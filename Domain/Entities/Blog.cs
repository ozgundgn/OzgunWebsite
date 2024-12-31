using Domain.Common;

namespace Domain.Entities
{
    public class Blog : BaseAuditableEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Link { get; set; }
        public byte[]? Image { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public IList<BlogPart> BlogParts { get; set; } = new List<BlogPart>();
    }
}
