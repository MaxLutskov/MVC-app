using GraphQL.Types;
using ToDOList2.Models;

namespace ToDOList2.GraphQL.GraphQLTypes
{
    public class TaskType : ObjectGraphType<TaskModel>
    {
        public TaskType()
        {
            Field<IntGraphType, int>()
                .Name("Id")
                .Resolve(x => x.Source.Id);
            Field<StringGraphType, string>()
                .Name("TaskDesc")
                .Resolve(x => x.Source.TaskDesc);
            Field<DateTimeGraphType, DateTime?>()
                .Name("Deadline")
                .Resolve(x => x.Source.Deadline);
            Field<BooleanGraphType, bool>()
                .Name("IsDone")
                .Resolve(x => x.Source.IsDone);
            Field<DateGraphType, DateTime?>()
                .Name("CompleteDate")
                .Resolve(x => x.Source.CompleteDate);
            Field<StringGraphType, string>()
                .Name("CategoryName")
                .Resolve(x => x.Source.CategoryName);
        }       
    }
}
