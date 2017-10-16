using SecondHand.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHand.Services.Data.Contracts
{
    public interface ICategoryService
    {
        IQueryable<Category> GetAll();
    }
}
