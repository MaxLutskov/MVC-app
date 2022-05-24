using ToDOList2.Models;

namespace ToDOList2.Interfaces
{
    public interface ITaskRepository
    {
        public List<TaskModel> GetTasks(string? category);
        public List<TaskModel> GetUncompliteTasks(List<TaskModel> tasks);
        public List<TaskModel> GetCompliteTasks(List<TaskModel> tasks);
        public TaskModel GetTask(int id);
        public void Add(TaskModel task);
        public void Delete(int id);
        public void UpdateState(int id);
        public void Update(TaskModel newTask, int id);
    }
}
