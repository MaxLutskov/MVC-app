using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace ToDOList2.Models
{
    public class Repository
    {
        string connectionString = "Server=DESKTOP-56K1OB0;Database=ToDoList;Trusted_Connection=True;MultipleActiveResultSets=true";
        
        public List<CategoryModel> GetCategories()
        {
            var categories = new List<CategoryModel>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                categories = db.Query<CategoryModel>("SELECT * FROM Category").ToList();
    }
            return categories;
        }
        public List<TaskModel> GetTasks(string? category)
        {
            var tasks = new List<TaskModel>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if(category != null && category != "All")
                {
                    tasks = db.Query<TaskModel>(@"SELECT * FROM Task WHERE CategoryName=@category ORDER BY Deadline", new { category }).ToList();
                    return tasks;
                }
                tasks = db.Query<TaskModel>(@"SELECT * FROM Task ORDER BY Deadline").ToList();
                return tasks;
            }
            
        }
        public List<TaskModel> GetUncompliteTasks(List<TaskModel> tasks)
        {
            var compTasks = new List<TaskModel>();
            foreach(TaskModel task in tasks)
            {
                if (!task.IsDone)
                {
                    compTasks.Add(task);
                }
            }
            return compTasks;
        }

        public List<TaskModel> GetCompliteTasks(List<TaskModel> tasks)
        {
            var compTasks = new List<TaskModel>();
            foreach (TaskModel task in tasks)
            {
                if (task.IsDone)
                {
                    compTasks.Add(task);
                }
            }
            return compTasks;
        }

        public TaskModel GetTask(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                TaskModel task = db.Query<TaskModel>(@"SELECT * FROM Task WHERE Id=@id", new{ id }).First();
                return task;
            }  
        }
            

        public void Add(TaskModel task)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Query(@"INSERT INTO Task (TaskDesc, Deadline, CategoryName) VALUES (@TaskDesc, @Deadline, @CategoryName)",
                new { task.TaskDesc, task.Deadline, task.CategoryName});
            }  
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Query(@"DELETE FROM Task WHERE Id = @id", new { id });
            }
        }
        public void UpdateState(int id)
        {
            var task = this.GetTask(id);
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                if (task.IsDone)
                    db.Query(@"UPDATE Task SET IsDone=0 WHERE Id = @Id", new { task.Id });
                else
                {
                    task.CompleteDate = DateTime.Now.Date;
                    db.Query(@"UPDATE Task SET IsDone=1, CompleteDate=@CompleteDate WHERE Id = @Id", 
                    new { task.CompleteDate , task.Id });
                    
                }
                    
            }
        }
    }
}
