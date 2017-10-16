using AutoMapper;
using Moq;
using NUnit.Framework;
using SecondHand.Data.Models;
using SecondHand.Services.Data.Contracts;
using SecondHand.Web.Controllers;
using SecondHand.Web.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;

namespace SecondHand.Web.UnitTests.Controllers
{
    [TestFixture]
    public class UserControllerTests
    {
        [Test]
        public void Constructor_Should_Throw_WhenProvidedNullUserService()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new UsersController(null, null, null));
        }

        [Test]
        public void Constructor_Should_Throw_WhenProvidedNullAdvertService()
        {
            // Arrange
            var userService = new Mock<IUsersService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new UsersController(userService.Object, null, null));
        }

        [Test]
        public void Constructor_Should_Throw_WhenProvidedNullMapper()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new UsersController(userService.Object, advertService.Object, null));
        }

        [Test]
        public void Constructor_Should_NotThrow_WhenProvidedValidDependencies()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var mapper = new Mock<IMapper>();

            // Act & Assert
            Assert.DoesNotThrow(() => new UsersController(userService.Object, advertService.Object, mapper.Object));
        }

        [Test]
        public void UserProfile_Should_CallUserServiceGetUserByUsernameWithTheGivenUsername()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var mapper = new Mock<IMapper>();

            string username = "user";

            userService.Setup(x => x.GetByUsername(It.IsAny<string>())).Returns<ApplicationUser>(null);

            var sut = new UsersController(userService.Object, advertService.Object, mapper.Object);

            // Act
            sut.UserProfile(username);

            // Assert
            userService.Verify(x => x.GetByUsername(username), Times.Once);
        }

        [Test]
        public void UserProfile_Should_RedirectToActionHomeIndexWhenUserIsNotFound()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var mapper = new Mock<IMapper>();

            string username = "user";

            userService.Setup(x => x.GetByUsername(It.IsAny<string>())).Returns<ApplicationUser>(null);

            var sut = new UsersController(userService.Object, advertService.Object, mapper.Object);

            // Act
            var result = (RedirectToRouteResult)sut.UserProfile(username);

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [Test]
        public void UserProfile_Should_CallMapperWithTheDbModelOnce()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var mapper = new Mock<IMapper>();

            string username = "user";
            var dbModel = new ApplicationUser();
            var viewModel = new UserProfileViewModel();

            userService.Setup(x => x.GetByUsername(It.IsAny<string>())).Returns(dbModel);
            mapper.Setup(x => x.Map<UserProfileViewModel>(dbModel)).Returns(viewModel);

            var sut = new UsersController(userService.Object, advertService.Object, mapper.Object);

            // Act
            sut.UserProfile(username);

            // Assert
            mapper.Verify(x => x.Map<UserProfileViewModel>(dbModel), Times.Once);
        }


        [Test]
        public void UserProfile_Should_ReturnTheDefaultViewWithTheViewModel()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var mapper = new Mock<IMapper>();

            string username = "user";
            var dbModel = new ApplicationUser();
            var viewModel = new UserProfileViewModel();

            userService.Setup(x => x.GetByUsername(It.IsAny<string>())).Returns(dbModel);
            mapper.Setup(x => x.Map<UserProfileViewModel>(dbModel)).Returns(viewModel);

            var sut = new UsersController(userService.Object, advertService.Object, mapper.Object);

            // Act
            sut.UserProfile(username);

            // Assert
            sut.WithCallTo(x => x.UserProfile(username))
                .ShouldRenderDefaultView()
                .WithModel(viewModel);
        }
    }
}
