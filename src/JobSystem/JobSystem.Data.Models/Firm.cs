using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSystem.Data.Models
{
    public class Firm : ApplicationUser
    {
        public string FirmName { get; set; }

        public virtual ICollection<Job> JobOffers { get; set; }
    }
}
