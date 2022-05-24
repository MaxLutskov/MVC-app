using GraphQL.Types;
using ToDOList2.GraphQL.GraphQLTypes;
using ToDOList2.Interfaces;
using ToDOList2.Models;
using ToDOList2.Service;

namespace ToDOList2.GraphQL.GraphQLQueries
{
    public class CategoryQueries : ObjectGraphType
    {
        private readonly ICategoryRepository repository;

        public CategoryQueries(IRepositoryResolver resolver)
        {
            repository = resolver.GetCategoryRepositoryByName(DataChecker.DataStorage);

            Field<ListGraphType<CategoryType>, List<CategoryModel>>()
                .Name("GetCategories")
                .Resolve(context => repository.GetCategories());
        }
    }
}
