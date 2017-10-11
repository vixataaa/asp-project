using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecondHand.Data.Models;

namespace SecondHand.Services.Data.Contracts
{
    public interface IJobsService
    {
        IEnumerable<Job> GetAll();

        IEnumerable<Job> GetAll(string query = "", int pageNumber = 1, int pageSize = 5);

        void AddJob(Job model);

        int RecordsCount(string query = "");

        Job GetById(Guid id);
    }
}
