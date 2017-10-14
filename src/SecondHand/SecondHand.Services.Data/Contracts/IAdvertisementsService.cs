using SecondHand.Data.Models;
using SecondHand.Services.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SecondHand.Services.Data.Contracts
{
    public interface IAdvertisementsService
    {
        int LastQueryRecordsCount { get; }

        void CreateAdvertisement(Advertisement adv, string categoryName);

        IQueryable<Advertisement> GetAdvertisements();

        IQueryable<Advertisement> GetAdvertisements(int pageNumber = 1, int pageSize = 5, string query = "");

        IQueryable<Advertisement> GetAdvertisements(int pageNumber = 1, int pageSize = 5, string query = "",
            string sortProperty = "", SortType sortType = SortType.Ascending, string category = "");

        IQueryable<Advertisement> GetUserAdvertisements(string username);

        Advertisement GetById(Guid id);

        void Edit(Advertisement adv);

        void Remove(Guid id);
    }
}
