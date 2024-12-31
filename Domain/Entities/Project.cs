using Domain.Common;

namespace Domain.Entities
{
    public class Project : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public List<string> Properties { get; set; }
        public List<string> Skills { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }   
    }
}
