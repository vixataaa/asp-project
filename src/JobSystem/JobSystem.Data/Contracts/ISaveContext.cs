using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSystem.Data.Contracts
{
    public interface ISaveContext
    {
        int SaveChanges();
    }
}
