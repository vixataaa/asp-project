using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSystem.Data.Models
{
    public class Person : ApplicationUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
