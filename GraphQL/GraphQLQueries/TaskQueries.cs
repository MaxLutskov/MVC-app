using GraphQL;
using GraphQL.Types;
using ToDOList2.GraphQL.GraphQLTypes;
using ToDOList2.Interfaces;
using ToDOList2.Models;
using ToDOList2.Service;

namespace ToDOList2.GraphQL.GraphQLQueries
{
    public class TaskQueries : ObjectGraphType
    {
        private readonly ITaskRepository repository;

        public TaskQueries(IRepositoryResolver resolver)
        {
            repository = resolver.GetTaskRepositoryByName(DataChecker.DataStorage);

            Field<ListGraphType<TaskType>, List<TaskModel>>()
                .Name("GetTasks")
                .Argument<StringGraphType, string?>("CategoryName", "Category for sort")
                .Resolve(context =>
                {
                    string? category = context.GetArgument<string?>("CategoryName");
                    return repository.GetTasks(category);
                });
            Field<TaskType, TaskModel>()
                .Name("GetTask")
                .Argument<IntGraphType, int>("Id", "Id of task")
                .Resolve(context =>
                {
                    int id = context.GetArgument<int>("Id");
                    return repository.GetTask(id);
                });
        }
    }
}
