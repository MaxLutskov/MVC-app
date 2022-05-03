

namespace ToDOList2.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string TaskDesc { get; set; }
        public DateTime? Deadline { get; set; }
        public bool IsDone { get; set; } = false;
        public DateTime? CompleteDate { get; set; }
        public string CategoryName { get; set; }
    }
}
