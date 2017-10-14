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
        private readonly ISaveContext context;
        private readonly ICategoryRepository categories;

        public CategoryService(ICategoryRepository categories, ISaveContext context)
        {
            Guard.WhenArgument(categories, "categories").IsNull().Throw();
            Guard.WhenArgument(context, "context").IsNull().Throw();

            this.categories = categories;
            this.context = context;
        }

        public void CreateCategory(string name)
        {
            var foundCategory = this.categories.GetCategoryByName(name);

            if (foundCategory == null)
            {
                this.categories.Add(new Category { Name = name });
                this.context.SaveChanges();
            }
        }

        public IQueryable<Category> GetAll()
        {
            return this.categories.All;
        }
    }
}
