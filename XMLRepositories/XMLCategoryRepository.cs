using ToDOList2.Interfaces;
using ToDOList2.Models;
using System.Xml.Linq;

namespace ToDOList2.XMLRepositories 
{
    public class XMLCategoryRepository : ICategoryRepository
    {
        string path = "XML/Categories.xml";
        public void Add(CategoryModel category)
        {
            var list = this.GetCategories();

            foreach(var el in list)
            {
                if (el.CategoryName == category.CategoryName)
                {
                    return;
                }
            }

            var doc = XDocument.Load(path);
            var newCategory = new XElement("Category",
                new XElement("CategoryName", category.CategoryName)
                );
            doc.Root.Add(newCategory);
            doc.Save(path);
        }

        public void Delete(string category)
        {
            var doc = XDocument.Load(path);

            foreach (var el in doc.Root.Elements())
            {
                foreach (var element in el.Elements())
                {
                    if (element.Value == category)
                    {
                       el.Remove();
                    }
                }
            }

            doc.Save(path);
        }

        public List<CategoryModel> GetCategories()
        {
            
            var categoryList = new List<CategoryModel>();

            var doc = XDocument.Load(path);

            foreach (var el in doc.Root.Elements())
            {
                foreach (var element in el.Elements())
                {
                    if(element.Name == "CategoryName")
                    {
                        var category = new CategoryModel();
                        category.CategoryName = element.Value;
                        categoryList.Add(category);
                    }  
                }
            }

            return categoryList;
        }

        public void Update(string categoryName, string newName)
        {
            var doc = XDocument.Load(path);

            foreach (var el in doc.Root.Elements())
            {
                foreach (var element in el.Elements())
                {
                    if(newName == null)
                    {
                        return;
                    }

                    if (element.Value == categoryName)
                    {
                        element.Value = newName;
                    }
                }
            }

            doc.Save(path);
        }
    }
}
