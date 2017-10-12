using SecondHand.Data.Models;
using SecondHand.Data.Repositories.Base;
using SecondHand.Data.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHand.Data.Repositories
{
    public class CategoryRepository : EfRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(MsSqlDbContext context) : base(context)
        {
        }

        public void CreateCategory(Category category)
        {
            this.Add(category);
        }

        public Category GetCategoryByName(string name)
        {
            var category = this.All.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());

            return category;
        }
    }
}
