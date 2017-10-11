using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecondHand.Data.Contracts;
using SecondHand.Data.Models;
using SecondHand.Services.Data.Contracts;

namespace SecondHand.Services.Data
{
    public class JobsService : IJobsService
    {
        private readonly IJobsRepository jobs;
        private readonly ISaveContext context;

        public JobsService(IJobsRepository jobs, ISaveContext context)
        {
            this.jobs = jobs;
            this.context = context;
        }

        public int RecordsCount(string query = "")
        {
            var jobs = this.jobs.All;

            if (!string.IsNullOrEmpty(query))
            {
                return jobs.Count(x => x.Title.ToLower().Contains(query) ||
                    x.AddedBy.UserName.ToLower().Contains(query));
            }

            return jobs.Count();
        }

        public void AddJob(Job model)
        {
            this.jobs.Add(model);
            this.context.SaveChanges();
        }

        public IEnumerable<Job> GetAll(string query = "", int pageNumber = 1, int pageSize = 5)
        {
            var result = this.jobs.All;

            if (!string.IsNullOrEmpty(query))
            {
                result = result.Where(x => x.Title.ToLower().Contains(query) ||
                    x.AddedBy.UserName.ToLower().Contains(query));
            }

            // Other types of sorting
            result = result
                .OrderBy(x => x.CreatedOn)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return result.ToList();
        }

        public IEnumerable<Job> GetAll()
        {
            return this.jobs.All.ToList();
        }

        public Job GetById(Guid id)
        {
            return this.jobs.All.FirstOrDefault(x => x.Id == id);
        }
    }
}
