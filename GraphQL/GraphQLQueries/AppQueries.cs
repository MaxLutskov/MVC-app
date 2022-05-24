using GraphQL.Types;
using ToDOList2.GraphQL.GraphQLTypes;
using ToDOList2.Interfaces;
using ToDOList2.Service;

namespace ToDOList2.GraphQL.GraphQLQueries
{
    public class AppQueries : ObjectGraphType
    {
        public AppQueries()
        {
            Field<TaskQueries>()
                .Name("Tasks")
                .Resolve(context => new {});
            Field<CategoryQueries>()
                .Name("Categories")
                .Resolve(context => new { });
        }

    }
}
