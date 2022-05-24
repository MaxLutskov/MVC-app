using GraphQL.Types;
using ToDOList2.GraphQL.GraphQLQueries;

namespace ToDOList2.GraphQL.GraphQLSchema
{
    public class AppSchema : Schema
    {
        public AppSchema(IServiceProvider provider)
            :base(provider)
        {
            Query = provider.GetRequiredService<AppQueries>();
        }
    }
}
