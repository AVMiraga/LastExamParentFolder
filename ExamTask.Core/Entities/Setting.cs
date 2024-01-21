using ExamTask.Core.Entities.Common;

namespace ExamTask.Core.Entities
{
    public class Setting : BaseEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
