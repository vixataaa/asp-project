using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHand.Data.Contracts
{
    public interface ISaveContext
    {
        int SaveChanges();
    }
}
