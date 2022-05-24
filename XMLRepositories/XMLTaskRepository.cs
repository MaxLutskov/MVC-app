using ToDOList2.Interfaces;
using ToDOList2.Models;
using System.Xml.Linq;

namespace ToDOList2.XMLRepositories
{
    public class XMLTaskRepository : ITaskRepository
    {
        string path = "XML/Tasks.xml";

        public List<TaskModel> GetTasks(string? category)
        {
            var doc = XDocument.Load(path);
            var tasks = new List<TaskModel>();
            List<XElement> list = new List<XElement>();

            if (category == null || category == "All")
            {
                list = doc.Root.Descendants("Task").ToList();   
            }
            else
            {
                list = doc.Root.Descendants("Task").Where(
                t => t.Element("CategoryName").Value == category).ToList();
            }

            foreach (XElement t in list)
            {
                DateTime? compDate = new DateTime();
                if (t.Element("CompleteDate").Value == "")
                    compDate = null;
                else
                    compDate = DateTime.Parse(t.Element("CompleteDate").Value);

                DateTime? deadline = new DateTime();
                if (t.Element("Deadline").Value == "")
                    deadline = null;
                else
                    deadline = DateTime.Parse(t.Element("Deadline").Value);

                var task = new TaskModel()
                {
                    Id = Int32.Parse(t.Attribute("Id").Value),
                    TaskDesc = t.Element("TaskDesc").Value,
                    Deadline = deadline,
                    IsDone = Convert.ToBoolean(t.Element("IsDone").Value),
                    CompleteDate = compDate,
                    CategoryName = t.Element("CategoryName").Value
                };

                tasks.Add(task);
            }

            return tasks;
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
            var doc = XDocument.Load(path);
            var list = doc.Root.Descendants("Task").ToList();

            foreach(var t in list)
            {
                if(t.Attribute("Id").Value == id.ToString())
                {
                    DateTime? date = new DateTime();
                    if (t.Element("CompleteDate").Value == "")
                        date = null;
                    else
                        date = DateTime.Parse(t.Element("CompleteDate").Value);

                    DateTime? deadline = new DateTime();
                    if (t.Element("Deadline").Value == "")
                        deadline = null;
                    else
                        deadline = DateTime.Parse(t.Element("Deadline").Value);

                    var task = new TaskModel()
                    {
                        Id = Int32.Parse(t.Attribute("Id").Value),
                        TaskDesc = t.Element("TaskDesc").Value,
                        Deadline = deadline,
                        IsDone = Convert.ToBoolean(t.Element("IsDone").Value),
                        CompleteDate = date,
                        CategoryName = t.Element("CategoryName").Value
                    };

                    return task;
                }
            }

            return null;
        }

        public void Add(TaskModel task)
        {
            var list = this.GetTasks(null);
            var lastId = list[list.Count-1].Id;

            var doc = XDocument.Load(path);

            var newTask = new XElement("Task",
                new XAttribute("Id", lastId+1),
                new XElement("TaskDesc", task.TaskDesc),
                new XElement("Deadline", task.Deadline),
                new XElement("IsDone", task.IsDone),
                new XElement("CompleteDate", task.CompleteDate),
                new XElement("CategoryName", task.CategoryName)
                );

            doc.Root.Add(newTask);
            doc.Save(path);
        }

        public void Delete(int id)
        {
            var doc = XDocument.Load(path);

            foreach (var el in doc.Root.Elements())
            {
                if (el.Attribute("Id").Value == id.ToString())
                {
                    el.Remove();
                }
            }

            doc.Save(path);
        }

        public void Update(TaskModel newTask, int id)
        {
            var task = this.GetTask(id);
            var doc = XDocument.Load(path);

            foreach (var el in doc.Root.Elements())
            {
                if (el.Attribute("Id").Value == id.ToString())
                {
                    el.Element("TaskDesc").Value = newTask.TaskDesc.ToString();
                    el.Element("Deadline").Value = newTask.Deadline.ToString();
                    el.Element("IsDone").Value = newTask.IsDone.ToString();
                    el.Element("CompleteDate").Value = newTask.CompleteDate.ToString();
                    el.Element("CategoryName").Value = newTask.CategoryName.ToString();
                }
            }


            doc.Save(path);
        }

        public void UpdateState(int id)
        {
            var task = this.GetTask(id);
            var doc = XDocument.Load(path);

            if (task.IsDone)
            {
                foreach(var el in doc.Root.Elements())
                {
                    if(el.Attribute("Id").Value == id.ToString())
                    {
                        el.Element("IsDone").Value = "false";
                    }
                }
            }
            else
            {
                task.CompleteDate = DateTime.Now.Date;
                foreach (var el in doc.Root.Elements())
                {
                    if (el.Attribute("Id").Value == id.ToString())
                    {
                        el.Element("IsDone").Value = "true";
                        el.Element("CompleteDate").Value = task.CompleteDate.ToString();
                    }
                }
            }

            doc.Save(path);
        }
    }
}
