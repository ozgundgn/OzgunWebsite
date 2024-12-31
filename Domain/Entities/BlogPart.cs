using Domain.Common;

namespace Domain.Entities
{
    public class BlogPart : BaseAuditableEntity
    {
        public int PartNo { get; set; }
        public string Text { get; set; }
        public IList<Image> Images { get; set; } = new List<Image>();
        public IList<CodeBlock> CodeBlocks { get; set; } = new List<CodeBlock>();
    }
}
