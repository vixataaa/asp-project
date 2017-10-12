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
        void CreateCategory(string name);

        IQueryable<Category> GetAll();
    }
}
