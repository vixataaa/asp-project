using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecondHand.Data.Models;

namespace SecondHand.ViewModels.Jobs
{
    public class JobsIndexViewModel
    {
        public IEnumerable<Job> Jobs { get; set; }

        public int ItemsCount { get; set; }

        public int CurrentPageNumber { get; set; }

        public int TotalPagesCount { get; set; }

        public string Query { get; set; }
    }
}
