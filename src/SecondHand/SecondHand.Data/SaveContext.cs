using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecondHand.Data.Contracts;
using Bytes2you.Validation;

namespace SecondHand.Data
{
    public class SaveContext : ISaveContext
    {
        private readonly MsSqlDbContext context;

        public SaveContext(MsSqlDbContext context)
        {
            Guard.WhenArgument(context, "context").IsNull().Throw();

            this.context = context;
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }
    }
}
