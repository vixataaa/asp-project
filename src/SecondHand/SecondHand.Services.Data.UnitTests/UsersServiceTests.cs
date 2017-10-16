using Moq;
using NUnit.Framework;
using SecondHand.Data.Models;
using SecondHand.Data.Repositories.Contracts;
using SecondHand.Services.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHand.Services.Data.UnitTests
{
    [TestFixture]
    public class UsersServiceTests
    {
        [Test]
        public void Constructor_Should_Throw_WhenProvidedNullUsersRepository()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new UsersService(null));
        }

        [Test]
        public void Constructor_Should_NotThrow_WhenProvidedValidUsersRepository()
        {
            // Arrange 
            var userRepo = new Mock<IUsersRepository>();

            // Act & Assert
            Assert.DoesNotThrow(() => new UsersService(userRepo.Object));
        }

        [Test]
        public void AllAndDeleted_Should_CallAndReturnUsersRepoAllAndDeleted()
        {
            // Arrange 
            var userRepo = new Mock<IUsersRepository>();

            var usersDbResult = new List<ApplicationUser>()
            {
                new ApplicationUser { }
            };

            userRepo.Setup(x => x.AllAndDeleted).Returns(usersDbResult.AsQueryable());

            var sut = new UsersService(userRepo.Object);

            // Act
            var result = sut.AllAndDeleted();

            // Assert
            userRepo.Verify(x => x.AllAndDeleted, Times.Once);
            Assert.AreEqual(usersDbResult, result);
        }

        [Test]
        public void GetById_Should_CallUserRepoWithTheGivenId()
        {
            // Arrange 
            var userRepo = new Mock<IUsersRepository>();

            string id = "string id";
            var user = new ApplicationUser();

            userRepo.Setup(x => x.GetById(It.IsAny<string>())).Returns(user);

            var sut = new UsersService(userRepo.Object);

            // Act
            var result = sut.GetById(id);

            // Assert
            userRepo.Verify(x => x.GetById(id), Times.Once);
        }

        [Test]
        public void GetById_Should_ReturnTheFoundUser()
        {
            // Arrange 
            var userRepo = new Mock<IUsersRepository>();

            string id = "string id";
            var user = new ApplicationUser();

            userRepo.Setup(x => x.GetById(It.IsAny<string>())).Returns(user);

            var sut = new UsersService(userRepo.Object);

            // Act
            var result = sut.GetById(id);

            // Assert
            Assert.AreEqual(user, result);
        }


        [Test]
        public void GetByUsername_Should_CallUserRepoWithTheGivenUsername()
        {
            // Arrange 
            var userRepo = new Mock<IUsersRepository>();

            string username = "string id";
            var user = new ApplicationUser();

            userRepo.Setup(x => x.GetByUsername(It.IsAny<string>())).Returns(user);

            var sut = new UsersService(userRepo.Object);

            // Act
            var result = sut.GetByUsername(username);

            // Assert
            userRepo.Verify(x => x.GetByUsername(username), Times.Once);
        }

        [Test]
        public void GetByUsername_Should_ReturnTheFoundUser()
        {
            // Arrange 
            var userRepo = new Mock<IUsersRepository>();

            string username = "string id";
            var user = new ApplicationUser();

            userRepo.Setup(x => x.GetByUsername(It.IsAny<string>())).Returns(user);

            var sut = new UsersService(userRepo.Object);

            // Act
            var result = sut.GetByUsername(username);

            // Assert
            Assert.AreEqual(user, result);
        }

        [Test]
        public void UpdateUser_Should_CallUserRepoUpdateOnceWithTheGivenModel()
        {
            // Arrange 
            var userRepo = new Mock<IUsersRepository>();

            var user = new ApplicationUser();

            userRepo.Setup(x => x.Update(user));

            var sut = new UsersService(userRepo.Object);

            // Act
            sut.UpdateUserProfile(user);

            // Assert
            userRepo.Verify(x => x.Update(user), Times.Once);
        }
    }
}
