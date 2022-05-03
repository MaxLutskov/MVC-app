namespace ToDOList2.Models
{
    public class TaskViewModel
    {
        public List<TaskModel>? CompliteTasks { get; set; }
        public List<TaskModel>? UncompliteTasks { get; set; }
        public List<CategoryModel>? Categories { get; set; }
        public TaskModel? TaskModel { get; set; }
    }
}
