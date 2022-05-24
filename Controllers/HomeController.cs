using Microsoft.AspNetCore.Mvc;
using ToDOList2.Interfaces;
using ToDOList2.Models;
using ToDOList2.ViewModels;
using ToDOList2.Service;

namespace ToDOList2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITaskRepository taskRepo;
        private readonly ICategoryRepository categoryRepo;

        public HomeController(IRepositoryResolver repositoryResolver)
        {
            taskRepo = repositoryResolver.GetTaskRepositoryByName(DataChecker.DataStorage); 
            categoryRepo = repositoryResolver.GetCategoryRepositoryByName(DataChecker.DataStorage); 
        }

        [HttpGet]
        public IActionResult Index(string? CategoryName)
        {
            var compliteTasks = new List<TaskModel>();
            var uncompliteTasks = new List<TaskModel>();
            var categories = new List<CategoryModel>();
            var tasks = taskRepo.GetTasks(CategoryName);
            var model = new TaskViewModel()
            {
                CompliteTasks = taskRepo.GetCompliteTasks(tasks),
                UncompliteTasks = taskRepo.GetUncompliteTasks(tasks),
                Categories = categoryRepo.GetCategories(),
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
            taskRepo.Add(task);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            taskRepo.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ChangeState(int id)
        {
            taskRepo.UpdateState(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = new TaskViewModel()
            {
                TaskModel = taskRepo.GetTask(id),
                Categories = categoryRepo.GetCategories(),
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(string newTaskDesc, DateTime newDeadline, string newCategoryName, int id)
        {
            var task = new TaskModel()
            {
                TaskDesc = newTaskDesc,
                Deadline = newDeadline,
                CategoryName = newCategoryName
            };
            taskRepo.Update(task, id);
            return RedirectToAction("Index");
        }

        public IActionResult ChangeStorage(string storage)
        {
            if(storage == "MSSQL")
            {
                DataChecker.DataStorage = storage;
            }
            else if (storage == "XML")
            {
                DataChecker.DataStorage = storage;
            }
            return RedirectToAction("Index");
        }

    }
}
