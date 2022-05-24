namespace ToDOList2.Interfaces
{
    public interface IRepositoryResolver
    {
        ICategoryRepository GetCategoryRepositoryByName(string name);
        ITaskRepository GetTaskRepositoryByName(string name);
    }
}
