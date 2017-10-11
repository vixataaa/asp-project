using JobSystem.Data.Contracts;
using JobSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSystem.Data.Repositories
{
    public class JobsRepository : EfRepository<Job>, IJobsRepository
    {
        public JobsRepository(MsSqlDbContext context) 
            : base(context)
        {
        }
    }
}
