using Domain.Common;

namespace Domain.Entities
{
    public class Image : BaseAuditableEntity
    {
        public int ImageNo { get; set; }
        public string ImageInfo { get; set; }
    }
}
