using JobSystem.Data.Common.Contracts;

namespace JobSystem.Data
{
    public interface IJobSystemData
    {
        IUsersRepository Users { get; }

        int SaveChanges();
    }
}
