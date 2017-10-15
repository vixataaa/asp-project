using SecondHand.Services.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecondHand.Data.Models;
using SecondHand.Data.Repositories.Contracts;
using SecondHand.Data.Contracts;
using Bytes2you.Validation;

namespace SecondHand.Services.Data
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categories;

        public CategoryService(ICategoryRepository categories)
        {
            Guard.WhenArgument(categories, "categories").IsNull().Throw();

            this.categories = categories;
        }

        public void CreateCategory(string name)
        {
            var foundCategory = this.categories.GetCategoryByName(name);

            if (foundCategory == null)
            {
                this.categories.Add(new Category { Name = name });
            }
        }

        public IQueryable<Category> GetAll()
        {
            return this.categories.All;
        }
    }
}
