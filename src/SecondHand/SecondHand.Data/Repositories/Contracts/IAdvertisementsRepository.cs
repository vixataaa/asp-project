using SecondHand.Data.Contracts;
using SecondHand.Data.Models;
using System;

namespace SecondHand.Data.Repositories.Contracts
{
    public interface IAdvertisementsRepository : IEfRepository<Advertisement>
    {
        void AddAdvertisement(Advertisement adv);

        void UpdateAdvertisement(Advertisement adv);

        void RemoveAdvertisement(Advertisement adv);

        Advertisement GetById(Guid id);
    }
}
