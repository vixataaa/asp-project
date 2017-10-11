using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecondHand.Data.Models;

namespace SecondHand.Data.Contracts
{
    public interface IJobsRepository : IEfRepository<Job>
    {
    }
}
