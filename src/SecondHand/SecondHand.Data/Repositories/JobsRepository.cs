using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecondHand.Data.Contracts;
using SecondHand.Data.Models;
using SecondHand.Data.Repositories.Base;

namespace SecondHand.Data.Repositories
{
    public class JobsRepository : EfRepository<Job>, IJobsRepository
    {
        public JobsRepository(MsSqlDbContext context) 
            : base(context)
        {
        }
    }
}
