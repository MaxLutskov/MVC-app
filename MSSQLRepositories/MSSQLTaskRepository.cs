using System.Data;
using System.Data.SqlClient;
using Dapper;
using ToDOList2.Interfaces;
using ToDOList2.Models;

namespace ToDOList2.MSSQLRepositories
{
    public class MSSQLTaskRepository : ITaskRepository
    {
        private readonly IDbConnection DbConnection;
        public MSSQLTaskRepository(IDbConnection dbConnection)
        {
            DbConnection = dbConnection;
        }

        public List<TaskModel> GetTasks(string? category)
        {
            var tasks = new List<TaskModel>();
            using (IDbConnection db = new SqlConnection(DbConnection.ConnectionString))
            {
                if (category != null && category != "All")
                {
                    tasks = db.Query<TaskModel>(@"SELECT * FROM Task WHERE CategoryName=@category ORDER BY Deadline", new {category}).ToList();
                    return tasks;
                }
                tasks = db.Query<TaskModel>(@"SELECT * FROM Task ORDER BY Deadline").ToList();
                return tasks;
            }

        }

        public List<TaskModel> GetUncompliteTasks(List<TaskModel> tasks)
        {
            var compTasks = new List<TaskModel>();
            foreach (TaskModel task in tasks)
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
            using (IDbConnection db = new SqlConnection(DbConnection.ConnectionString))
            {
                TaskModel task = db.Query<TaskModel>(@"SELECT * FROM Task WHERE Id=@id", new {id}).First();
                return task;
            }
        }

        public void Add(TaskModel task)
        {
            using (IDbConnection db = new SqlConnection(DbConnection.ConnectionString))
            {
                db.Query(@"INSERT INTO Task (TaskDesc, Deadline, CategoryName) VALUES (@TaskDesc, @Deadline, @CategoryName)", new {task.TaskDesc, task.Deadline, task.CategoryName});
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(DbConnection.ConnectionString))
            {
                db.Query(@"DELETE FROM Task WHERE Id=@id", new {id});
            }
        }

        public void UpdateState(int id)
        {
            var task = this.GetTask(id);
            using (IDbConnection db = new SqlConnection(DbConnection.ConnectionString))
            {
                if (task.IsDone)
                    db.Query(@"UPDATE Task SET IsDone=0 WHERE Id=@id", new {id});
                else
                {
                    task.CompleteDate = DateTime.Now.Date;
                    db.Query(@"UPDATE Task SET IsDone=1, CompleteDate=@CompleteDate WHERE Id=@id", 
                    new {task.CompleteDate, task.Id});
                }
            }
        }

        public void Update(TaskModel newTask, int id)
        {
            using (IDbConnection db = new SqlConnection(DbConnection.ConnectionString))
            {
                    db.Query(@"UPDATE Task SET TaskDesc=@TaskDesc, Deadline=@Deadline, CategoryName=@CategoryName WHERE Id=@id",
                    new { newTask.TaskDesc, newTask.Deadline, newTask.CategoryName, id });
            }
        }
    }
}
