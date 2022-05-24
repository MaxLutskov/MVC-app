using GraphQL.Types;
using ToDOList2.Models;

namespace ToDOList2.GraphQL.GraphQLTypes
{
    public class CategoryType : ObjectGraphType<CategoryModel>
    {
        public CategoryType()
        {
            Field<StringGraphType>()
                .Name("CategoryName")
                .Resolve(x => x.Source.CategoryName);
        }
    }
}
