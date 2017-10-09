using System;
using System.Collections.Generic;
using JobSystem.Data.Common.Contracts;
using JobSystem.Data.Repositories.Factories;
using JobSystem.Data.Models.Contracts;
using JobSystem.Data.Models;

namespace JobSystem.Data
{
    public class JobSystemData : IJobSystemData
    {
        private readonly MsSqlDbContext context;

        private readonly Dictionary<Type, object> repositories;
        private readonly IRepositoryFactory repositoryFactory;

        public JobSystemData(MsSqlDbContext context, IRepositoryFactory repositoryFactory)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
            this.repositoryFactory = repositoryFactory;
        }

        public IUsersRepository Users
        {
            get
            {
                return (IUsersRepository)this.GetRepository<ApplicationUser>();
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IEfRepository<T> GetRepository<T>()
            where T : class, IDeletable, IAuditable
        {
            return this.repositoryFactory.GetRepository<T>();
        }
    }
}
