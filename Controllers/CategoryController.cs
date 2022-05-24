using Microsoft.AspNetCore.Mvc;
using ToDOList2.Interfaces;
using ToDOList2.ViewModels;
using ToDOList2.Models;

namespace ToDOList2.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepo;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            categoryRepo = categoryRepository;
        }

        public IActionResult Index()
        {
            var categories = categoryRepo.GetCategories();
            var model = new CategoryViewModel
            {
                Categories = categories
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(string categoryName)
        {
            var category = new CategoryModel
            {
                CategoryName = categoryName
            };
            categoryRepo.Add(category);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            categoryRepo.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(string categoryName, string newName)
        {
            categoryRepo.Update(categoryName, newName);
            return RedirectToAction("Index");
        }
    }
}
