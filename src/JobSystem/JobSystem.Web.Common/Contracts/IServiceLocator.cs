using JobSystem.Data.Common.Contracts;
using JobSystem.Data.Models.Contracts;

namespace JobSystem.Web.Common.Contracts
{
    public interface IServiceLocator
    {
        IEfRepository<T> GetRepository<T>() 
            where T : class, IDeletable, IAuditable;
    }
}
