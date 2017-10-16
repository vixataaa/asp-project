using AutoMapper;
using Kendo.Mvc.UI;
using Moq;
using NUnit.Framework;
using SecondHand.Data.Models;
using SecondHand.Services.Data.Contracts;
using SecondHand.Web.Areas.Administration.Controllers;
using SecondHand.Web.Areas.Administration.Models.UsersPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace SecondHand.Web.UnitTests.Areas.Administration.Controllers
{
    [TestFixture]
    public class UsersPanelControllerTests
    {
        [Test]
        public void Constructor_Should_Throw_WhenGivenNullUserService()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new UsersPanelController(null, null));
        }

        [Test]
        public void Constructor_Should_Throw_WhenGivenNullMapper()
        {
            // Arrange
            var userService = new Mock<IAdminUsersService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new UsersPanelController(userService.Object, null));
        }

        [Test]
        public void Constructor_ShouldNot_Throw_WhenGivenValidDependencies()
        {
            // Arrange
            var userService = new Mock<IAdminUsersService>();
            var mapper = new Mock<IMapper>();

            // Act & Assert
            Assert.DoesNotThrow(() => new UsersPanelController(userService.Object, mapper.Object));
        }

        [Test]
        public void Index_Should_ReturnDefaultView()
        {
            // Arrange
            var userService = new Mock<IAdminUsersService>();
            var mapper = new Mock<IMapper>();

            var sut = new UsersPanelController(userService.Object, mapper.Object);

            // Act
            sut.Index();

            // Assert
            sut.WithCallTo(x => x.Index())
                .ShouldRenderDefaultView();
        }

        [Test]
        public void GetUsers_Should_CallUserServiceAllAndDeleted()
        {
            // Arrange
            var userService = new Mock<IAdminUsersService>();
            var mapper = new Mock<IMapper>();

            var getUsersResult = new List<ApplicationUser>()
            {
                new ApplicationUser { }
            };

            var mapUsersResult = new UserGridViewModel { };

            userService.Setup(x => x.AllAndDeleted()).Returns(getUsersResult.AsQueryable());
            mapper.Setup(x => x.Map<UserGridViewModel>(getUsersResult)).Returns(mapUsersResult);

            var request = new DataSourceRequest();

            var sut = new UsersPanelController(userService.Object, mapper.Object);

            // Act
            sut.GetUsers(request);

            // Assert
            userService.Verify(x => x.AllAndDeleted(), Times.Once);
        }

        [Test]
        public void GetUsers_Should_ReturnJsonResult()
        {
            // Arrange
            var userService = new Mock<IAdminUsersService>();
            var mapper = new Mock<IMapper>();

            var getUsersResult = new List<ApplicationUser>()
            {
                new ApplicationUser { }
            };

            var mapUsersResult = new UserGridViewModel { };

            userService.Setup(x => x.AllAndDeleted()).Returns(getUsersResult.AsQueryable());
            mapper.Setup(x => x.Map<UserGridViewModel>(getUsersResult)).Returns(mapUsersResult);
            var request = new DataSourceRequest();

            var sut = new UsersPanelController(userService.Object, mapper.Object);

            // Act
            sut.GetUsers(request);

            // Assert
            sut.WithCallTo(x => x.GetUsers(request))
                .ShouldReturnJson();
        }

        [Test]
        public void EditUser_Should_CallMapperWithThePassedModelOnce()
        {
            // Arrange
            var userService = new Mock<IAdminUsersService>();
            var mapper = new Mock<IMapper>();

            var dbModel = new ApplicationUser { };

            var viewModel = new UserGridViewModel { };

            userService.Setup(x => x.UpdateUserProfile(dbModel));
            mapper.Setup(x => x.Map<ApplicationUser>(viewModel)).Returns(dbModel);

            var request = new DataSourceRequest();

            var sut = new UsersPanelController(userService.Object, mapper.Object);

            // Act
            sut.EditUser(viewModel);

            // Assert
            mapper.Verify(x => x.Map<ApplicationUser>(viewModel), Times.Once);
        }

        [Test]
        public void EditUser_Should_UserServiceUpdateUserProfileOnceWithProperParameters()
        {
            // Arrange
            var userService = new Mock<IAdminUsersService>();
            var mapper = new Mock<IMapper>();

            var dbModel = new ApplicationUser { };

            var viewModel = new UserGridViewModel { };

            userService.Setup(x => x.UpdateUserProfile(dbModel));
            mapper.Setup(x => x.Map<ApplicationUser>(viewModel)).Returns(dbModel);

            var request = new DataSourceRequest();

            var sut = new UsersPanelController(userService.Object, mapper.Object);

            // Act
            sut.EditUser(viewModel);

            // Assert
            userService.Verify(x => x.UpdateUserProfile(dbModel), Times.Once);
        }


        [Test]
        public void EditUser_Should_ReturnJsonResult()
        {
            // Arrange
            var userService = new Mock<IAdminUsersService>();
            var mapper = new Mock<IMapper>();

            var dbModel = new ApplicationUser { };

            var viewModel = new UserGridViewModel { };

            userService.Setup(x => x.UpdateUserProfile(dbModel));
            mapper.Setup(x => x.Map<ApplicationUser>(viewModel)).Returns(dbModel);

            var request = new DataSourceRequest();

            var sut = new UsersPanelController(userService.Object, mapper.Object);

            // Act
            sut.EditUser(viewModel);

            // Assert
            sut.WithCallTo(x => x.EditUser(viewModel))
                .ShouldReturnJson();
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void DeleteUser_Should_CallMapperWithThePassedModelOnce()
        {
            // Arrange
            var userService = new Mock<IAdminUsersService>();
            var mapper = new Mock<IMapper>();

            var dbModel = new ApplicationUser { };

            var viewModel = new UserGridViewModel { };

            userService.Setup(x => x.DeleteUser(dbModel));
            mapper.Setup(x => x.Map<ApplicationUser>(viewModel)).Returns(dbModel);

            var request = new DataSourceRequest();

            var sut = new UsersPanelController(userService.Object, mapper.Object);

            // Act
            sut.DeleteUser(viewModel);

            // Assert
            mapper.Verify(x => x.Map<ApplicationUser>(viewModel), Times.Once);
        }

        [Test]
        public void DeleteUser_Should_CallUserServiceDeleteUserOnceWithTheProperModel()
        {
            // Arrange
            var userService = new Mock<IAdminUsersService>();
            var mapper = new Mock<IMapper>();

            var dbModel = new ApplicationUser { };

            var viewModel = new UserGridViewModel { };

            userService.Setup(x => x.DeleteUser(dbModel));
            mapper.Setup(x => x.Map<ApplicationUser>(viewModel)).Returns(dbModel);

            var request = new DataSourceRequest();

            var sut = new UsersPanelController(userService.Object, mapper.Object);

            // Act
            sut.DeleteUser(viewModel);

            // Assert
            userService.Verify(x => x.DeleteUser(dbModel), Times.Once);
        }


        [Test]
        public void DeleteUser_Should_ReturnJsonResult()
        {
            // Arrange
            var userService = new Mock<IAdminUsersService>();
            var mapper = new Mock<IMapper>();

            var dbModel = new ApplicationUser { };

            var viewModel = new UserGridViewModel { };

            userService.Setup(x => x.DeleteUser(dbModel));
            mapper.Setup(x => x.Map<ApplicationUser>(viewModel)).Returns(dbModel);

            var request = new DataSourceRequest();

            var sut = new UsersPanelController(userService.Object, mapper.Object);

            // Act
            sut.DeleteUser(viewModel);

            // Assert
            sut.WithCallTo(x => x.DeleteUser(viewModel))
                .ShouldReturnJson();
        }
    }
}
