using SecondHand.Data.Contracts;
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
    public class AdvertisementsRepository : EfRepository<Advertisement>, IAdvertisementsRepository
    {
        public AdvertisementsRepository(MsSqlDbContext context) 
            : base(context)
        {
        }

        public void AddAdvertisement(Advertisement adv)
        {
            this.Add(adv);
        }

        public Advertisement GetById(Guid id)
        {
            var adv = this.All.FirstOrDefault(x => x.Id == id);

            return adv;
        }

        public void RemoveAdvertisement(Advertisement adv)
        {
            this.Delete(adv);
        }

        public void UpdateAdvertisement(Advertisement adv)
        {
            this.Update(adv);
        }
    }
}
