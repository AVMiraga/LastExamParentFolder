using ExamTask.Core.Entities.Common;

namespace ExamTask.Core.Entities
{
    public class Portfolio : BaseEntity
    {
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
    }
}
