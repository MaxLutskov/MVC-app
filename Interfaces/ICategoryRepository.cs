using ToDOList2.Models;

namespace ToDOList2.Interfaces
{
    public interface ICategoryRepository
    {
        public List<CategoryModel> GetCategories();
        public void Add(CategoryModel category);
        public void Delete(string category);
        public void Update(string categoryName, string newName);
    }
}
