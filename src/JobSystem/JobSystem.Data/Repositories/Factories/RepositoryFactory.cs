using JobSystem.Data.Models.Contracts;
using JobSystem.Data.Common.Contracts;
using JobSystem.Web.Common.Contracts;
using JobSystem.Data.Models;

namespace JobSystem.Data.Repositories.Factories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IServiceLocator serviceLocator;

        public RepositoryFactory(IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        public IEfRepository<T> GetRepository<T>()
            where T : class, IDeletable, IAuditable
        {
            return serviceLocator.GetRepository<T>();
        }
    }
}
