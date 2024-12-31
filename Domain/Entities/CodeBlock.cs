using Domain.Common;

namespace Domain.Entities
{
    public class CodeBlock:BaseAuditableEntity
    {
        public int CodeNo { get; set; }
        public string Code { get; set; }
    }
}
