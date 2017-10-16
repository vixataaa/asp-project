using Moq;
using NUnit.Framework;
using SecondHand.Data.Models;
using SecondHand.Data.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHand.Services.Data.UnitTests
{
    [TestFixture]
    public class AdvertisementsServiceTests
    {
        [Test]
        public void Constructor_Should_Throw_WhenGivenNullAdvertRepo()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new AdvertisementsService(null, null, null));
        }

        [Test]
        public void Constructor_Should_Throw_WhenGivenNullCategoryRepo()
        {
            // Arrange 
            var advRepo = new Mock<IAdvertisementsRepository>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new AdvertisementsService(advRepo.Object, null, null));
        }

        [Test]
        public void Constructor_Should_Throw_WhenGivenNullUserRepo()
        {
            // Arrange 
            var advRepo = new Mock<IAdvertisementsRepository>();
            var catRepo = new Mock<ICategoryRepository>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new AdvertisementsService(advRepo.Object, catRepo.Object, null));
        }


        [Test]
        public void Constructor_Should_NotThrow_WhenGivenValidDependencies()
        {
            // Arrange 
            var advRepo = new Mock<IAdvertisementsRepository>();
            var catRepo = new Mock<ICategoryRepository>();
            var userRepo = new Mock<IUsersRepository>();

            // Act & Assert
            Assert.DoesNotThrow(() => new AdvertisementsService(advRepo.Object, catRepo.Object, userRepo.Object));
        }

        [Test]
        public void Edit_Should_Call_AdsRepoUpdateOnceWithTheGivenAdvert()
        {
            // Arrange 
            var advRepo = new Mock<IAdvertisementsRepository>();
            var catRepo = new Mock<ICategoryRepository>();
            var userRepo = new Mock<IUsersRepository>();

            var adv = new Advertisement();

            advRepo.Setup(x => x.Update(adv));

            var sut = new AdvertisementsService(advRepo.Object, catRepo.Object, userRepo.Object);

            // Act
            sut.Edit(adv);

            // Assert
            advRepo.Verify(x => x.Update(adv), Times.Once);
        }

        [Test]
        public void Remove_Should_Call_AdsRepoGetByIdOnceWithTheGivenAdvertId()
        {
            // Arrange 
            var advRepo = new Mock<IAdvertisementsRepository>();
            var catRepo = new Mock<ICategoryRepository>();
            var userRepo = new Mock<IUsersRepository>();

            var id = new Guid();

            var adv = new Advertisement();

            advRepo.Setup(x => x.GetById(id)).Returns(adv);
            advRepo.Setup(x => x.Delete(adv));

            var sut = new AdvertisementsService(advRepo.Object, catRepo.Object, userRepo.Object);

            // Act
            sut.Remove(id);

            // Assert
            advRepo.Verify(x => x.GetById(id), Times.Once);
        }

        [Test]
        public void Remove_Should_Call_AdsRepoDeleteIfAdvertIsFound()
        {
            // Arrange 
            var advRepo = new Mock<IAdvertisementsRepository>();
            var catRepo = new Mock<ICategoryRepository>();
            var userRepo = new Mock<IUsersRepository>();

            var id = new Guid();

            var adv = new Advertisement();

            advRepo.Setup(x => x.GetById(id)).Returns(adv);
            advRepo.Setup(x => x.Delete(adv));

            var sut = new AdvertisementsService(advRepo.Object, catRepo.Object, userRepo.Object);

            // Act
            sut.Remove(id);

            // Assert
            advRepo.Verify(x => x.Delete(adv), Times.Once);
        }

        [Test]
        public void Remove_Should_NotCall_AdsRepoDeleteIfAdvertIsNOTFound()
        {
            // Arrange 
            var advRepo = new Mock<IAdvertisementsRepository>();
            var catRepo = new Mock<ICategoryRepository>();
            var userRepo = new Mock<IUsersRepository>();

            var id = new Guid();

            advRepo.Setup(x => x.GetById(id)).Returns<Advertisement>(null);
            advRepo.Setup(x => x.Delete(It.IsAny<Advertisement>()));

            var sut = new AdvertisementsService(advRepo.Object, catRepo.Object, userRepo.Object);

            // Act
            sut.Remove(id);

            // Assert
            advRepo.Verify(x => x.Delete(It.IsAny<Advertisement>()), Times.Never);
        }

        [Test]
        public void GetAdvertisements_Should_Call_AdvRepoAllOnceAndReturnIt()
        {
            // Arrange 
            var advRepo = new Mock<IAdvertisementsRepository>();
            var catRepo = new Mock<ICategoryRepository>();
            var userRepo = new Mock<IUsersRepository>();

            var ads = new List<Advertisement>().AsQueryable();

            advRepo.Setup(x => x.All).Returns(ads);

            var sut = new AdvertisementsService(advRepo.Object, catRepo.Object, userRepo.Object);

            // Act
            var result = sut.GetAdvertisements();

            // Assert
            advRepo.Verify(x => x.All, Times.Once);
            Assert.AreEqual(ads, result);
        }

        [Test]
        public void GetById_Should_Call_AdvRepoGetByIdOnceAndTheFoundResult()
        {
            // Arrange 
            var advRepo = new Mock<IAdvertisementsRepository>();
            var catRepo = new Mock<ICategoryRepository>();
            var userRepo = new Mock<IUsersRepository>();

            var ad = new Advertisement();
            var id = new Guid();

            advRepo.Setup(x => x.GetById(id)).Returns(ad);

            var sut = new AdvertisementsService(advRepo.Object, catRepo.Object, userRepo.Object);

            // Act
            var result = sut.GetById(id);

            // Assert
            advRepo.Verify(x => x.GetById(id), Times.Once);
            Assert.AreEqual(ad, result);
        }

        [Test]
        public void GetByUserAdvertisements_Should_ReturnCorrectResult()
        {
            // Arrange 
            var advRepo = new Mock<IAdvertisementsRepository>();
            var catRepo = new Mock<ICategoryRepository>();
            var userRepo = new Mock<IUsersRepository>();

            var username = "user";

            var ads = new List<Advertisement>()
            {
                new Advertisement
                {
                    AddedBy = new ApplicationUser { UserName = username }
                },
                new Advertisement
                {
                    AddedBy = new ApplicationUser { UserName = "other user" }
                },
                new Advertisement
                {
                    AddedBy = new ApplicationUser { UserName = username }
                }
            }.AsQueryable();

            advRepo.Setup(x => x.All).Returns(ads);

            var sut = new AdvertisementsService(advRepo.Object, catRepo.Object, userRepo.Object);

            // Act
            var result = sut.GetUserAdvertisements(username);

            // Assert
            advRepo.Verify(x => x.All, Times.Once);
            Assert.AreEqual(2, result.Count());
        }


        [Test]
        public void AllAndDeleted_Should_Call_AdvRepoAllAndDeletedAndReturnIt()
        {
            // Arrange 
            var advRepo = new Mock<IAdvertisementsRepository>();
            var catRepo = new Mock<ICategoryRepository>();
            var userRepo = new Mock<IUsersRepository>();

            var ads = new List<Advertisement>().AsQueryable();

            advRepo.Setup(x => x.AllAndDeleted).Returns(ads);

            var sut = new AdvertisementsService(advRepo.Object, catRepo.Object, userRepo.Object);

            // Act
            var result = sut.AllAndDeleted();

            // Assert
            advRepo.Verify(x => x.AllAndDeleted, Times.Once);
            Assert.AreEqual(ads, result);
        }


        [Test]
        public void Remove_Should_Call_AdvRepoDeleteWithTheGivenAdvertisement()
        {
            // Arrange 
            var advRepo = new Mock<IAdvertisementsRepository>();
            var catRepo = new Mock<ICategoryRepository>();
            var userRepo = new Mock<IUsersRepository>();

            var ad = new Advertisement();

            advRepo.Setup(x => x.Delete(ad));

            var sut = new AdvertisementsService(advRepo.Object, catRepo.Object, userRepo.Object);

            // Act
            sut.Remove(ad);

            // Assert
            advRepo.Verify(x => x.Delete(ad), Times.Once);
        }
    }
}
