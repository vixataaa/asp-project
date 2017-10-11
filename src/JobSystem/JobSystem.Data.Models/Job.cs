using JobSystem.Data.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSystem.Data.Models
{
    public class Job : DataModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public virtual Firm AddedBy { get; set; }
    }
}
