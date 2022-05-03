using Microsoft.AspNetCore.Mvc;
using ToDOList2.Models;

namespace ToDOList2.Controllers
{
    public class HomeController : Controller
    {
        Repository repository = new Repository();
        List<TaskModel> tasks = new List<TaskModel>();


        [HttpGet]
        public IActionResult Index(string? CategoryName)
        {
            var compliteTasks = new List<TaskModel>();
            var uncompliteTasks = new List<TaskModel>();
            var categories = new List<CategoryModel>();
            tasks = repository.GetTasks(CategoryName);
            var model = new TaskViewModel()
            {
                CompliteTasks = repository.GetCompliteTasks(tasks),
                UncompliteTasks = repository.GetUncompliteTasks(tasks),
                Categories = repository.GetCategories(),
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(string taskDesc, DateTime? deadline, string categoryName)
        {
            TaskModel task = new TaskModel()
            {
                TaskDesc = taskDesc,
                Deadline = deadline,
                CategoryName = categoryName
            };
            repository.Add(task);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            repository.Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult ChangeState(int id)
        {
            repository.UpdateState(id);
            return RedirectToAction("Index");
        }

    }
}
