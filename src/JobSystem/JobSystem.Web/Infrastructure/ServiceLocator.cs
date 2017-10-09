using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using JobSystem.Web.Common.Contracts;
using JobSystem.Data.Models.Contracts;
using JobSystem.Data.Repositories;
using JobSystem.Data.Models;
using JobSystem.Data.Common.Contracts;

namespace JobSystem.Web.Infrastructure
{
    public class ServiceLocator : IServiceLocator
    {
        private readonly IKernel kernel;

        public ServiceLocator(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public IEfRepository<T> GetRepository<T>() 
            where T : class, IDeletable, IAuditable
        {
            var type = typeof(EfRepository<T>);

            if (typeof(T).IsAssignableFrom(typeof(ApplicationUser)))
            {
                type = typeof(UsersRepository);
            }

            // More cases
            return (IEfRepository<T>)this.kernel.Get(type);
        }
    }
}