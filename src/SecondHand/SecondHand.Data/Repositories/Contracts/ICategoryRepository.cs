using SecondHand.Data.Contracts;
using SecondHand.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHand.Data.Repositories.Contracts
{
    public interface ICategoryRepository : IEfRepository<Category>
    {
        void CreateCategory(Category category);

        Category GetCategoryByName(string name);
    }
}
