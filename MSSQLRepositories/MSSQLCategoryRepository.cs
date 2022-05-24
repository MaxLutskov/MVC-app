using Dapper;
using System.Data;
using System.Data.SqlClient;
using ToDOList2.Interfaces;
using ToDOList2.Models;

namespace ToDOList2.MSSQLRepositories
{
    public class MSSQLCategoryRepository : ICategoryRepository
    {
        private readonly IDbConnection DbConnection;
        public MSSQLCategoryRepository(IDbConnection dbConnection)
        {
            DbConnection = dbConnection;
        }

        public List<CategoryModel> GetCategories()
        {
            var categories = new List<CategoryModel>();
            using (IDbConnection db = new SqlConnection(DbConnection.ConnectionString))
            {
                categories = db.Query<CategoryModel>("SELECT * FROM Category").ToList();
            }
            return categories;
        }

        public void Add(CategoryModel category)
        {
            using (IDbConnection db = new SqlConnection(DbConnection.ConnectionString))
            {
                db.Query(@"INSERT INTO Category (CategoryName) VALUES (@CategoryName)", new {category.CategoryName});
            }
        }

        public void Delete(string category)
        {
            using (IDbConnection db = new SqlConnection(DbConnection.ConnectionString))
            {
                db.Query(@"DELETE FROM Category WHERE CategoryName=@category", new {category});
            }
        }

        public void Update(string categoryName, string newName)
        {
            if(newName == null)
            {
                return;
            }

            using (IDbConnection db = new SqlConnection(DbConnection.ConnectionString))
            {
                db.Query(@"UPDATE Category SET CategoryName=@newName WHERE CategoryName=@CategoryName", new {newName, categoryName});
            }
        }

    }
}
