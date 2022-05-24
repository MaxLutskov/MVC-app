using ToDOList2.Interfaces;

namespace ToDOList2.Service
{
    public class RepositoryResolver : IRepositoryResolver
    {
        private readonly IServiceProvider serviceProvider;

        public RepositoryResolver(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ICategoryRepository GetCategoryRepositoryByName(string name)
        {
            if (name == "MSSQL")
                return serviceProvider.GetService<MSSQLRepositories.MSSQLCategoryRepository>();
            else if (name == "XML")
                return serviceProvider.GetService<XMLRepositories.XMLCategoryRepository>();
            else 
                return null;
        }

        public ITaskRepository GetTaskRepositoryByName(string name)
        {
            if (name == "MSSQL")
                return serviceProvider.GetService<MSSQLRepositories.MSSQLTaskRepository>();
            else if (name == "XML")
                return serviceProvider.GetService<XMLRepositories.XMLTaskRepository>();
            else
                return null;
        }
    }
}
