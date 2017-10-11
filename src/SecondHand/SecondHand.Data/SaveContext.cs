using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecondHand.Data.Contracts;

namespace SecondHand.Data
{
    public class SaveContext : ISaveContext
    {
        private readonly MsSqlDbContext context;

        public SaveContext(MsSqlDbContext context)
        {
            this.context = context;
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }
    }
}
