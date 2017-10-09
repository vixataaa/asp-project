using JobSystem.Data.Common.Contracts;
using JobSystem.Data.Models.Contracts;

namespace JobSystem.Data.Repositories.Factories
{
    public interface IRepositoryFactory
    {
        IEfRepository<T> GetRepository<T>()
            where T : class, IDeletable, IAuditable;
    }
}
