using SecondHand.Services.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecondHand.Data.Models;
using SecondHand.Data.Repositories.Contracts;
using SecondHand.Data.Contracts;
using SecondHand.Services.Data.Common;

namespace SecondHand.Services.Data
{
    public class AdvertisementsService : IAdvertisementsService
    {
        private readonly ICategoryRepository categories;
        private readonly IAdvertisementsRepository advertisements;
        private readonly ISaveContext context;
        private int lastQueryRecordsCount;

        public AdvertisementsService(IAdvertisementsRepository advertisements, ICategoryRepository categories, ISaveContext context)
        {
            this.advertisements = advertisements;
            this.categories = categories;
            this.context = context;
            this.lastQueryRecordsCount = 0;
        }

        public int LastQueryRecordsCount
        {
            get
            {
                return this.lastQueryRecordsCount;
            }
        }

        public void CreateAdvertisement(Advertisement adv, string categoryName)
        {
            var category = this.categories.GetCategoryByName(categoryName);

            if (category != null)
            {
                adv.Category = category;

                foreach (var photo in adv.Photos)
                {
                    photo.Advertisement = adv;
                }

                this.advertisements.Add(adv);
                this.context.SaveChanges();
            }
        }

        public IQueryable<Advertisement> GetAdvertisements()
        {
            return this.advertisements.All;
        }

        public IQueryable<Advertisement> GetAdvertisements(int pageNumber = 1, int pageSize = 5, string query = "")
        {
            var result = this.GetAdvertisements(pageNumber, pageSize, query, "", SortType.Ascending, "");

            return result;
        }

        public IQueryable<Advertisement> GetAdvertisements(int pageNumber = 1, int pageSize = 5, string query = "",
            string sortProperty = "", SortType sortType = SortType.Ascending, string category = "")
        {
            var result = this.advertisements.All;

            if (!string.IsNullOrEmpty(query))
            {
                result = result.Where(x =>
                    x.Title.ToLower().Contains(query.ToLower()) ||
                    x.Description.ToLower().Contains(query.ToLower()) ||
                    x.Category.Name.ToLower().Contains(query.ToLower()));
            }

            if (!string.IsNullOrEmpty(category))
            {
                result = result.Where(x => x.Category.Name.ToLower() == category.ToLower());
            }

            switch (sortProperty.ToLower())
            {
                case "title":
                    if (sortType == SortType.Ascending)
                    {
                        result = result.OrderBy(x => x.Title);
                    }
                    else
                    {
                        result = result.OrderByDescending(x => x.Title);
                    }
                    break;
                case "price":
                    if (sortType == SortType.Ascending)
                    {
                        result = result.OrderBy(x => x.Price);
                    }
                    else
                    {
                        result = result.OrderByDescending(x => x.Price);
                    }
                    break;
                default:
                    if (sortType == SortType.Ascending)
                    {
                        result = result.OrderBy(x => x.CreatedOn);
                    }
                    else
                    {
                        result = result.OrderByDescending(x => x.CreatedOn);
                    }
                    break;
            }

            this.lastQueryRecordsCount = result.Count();

            result = result
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return result;
        }

        public Advertisement GetById(Guid id)
        {
            return this.advertisements.GetById(id);
        }
    }
}
