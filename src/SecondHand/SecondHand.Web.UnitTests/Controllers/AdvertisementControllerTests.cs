using AutoMapper;
using Moq;
using NUnit.Framework;
using SecondHand.Data.Models;
using SecondHand.Services.Data.Common;
using SecondHand.Services.Data.Contracts;
using SecondHand.Web.Controllers;
using SecondHand.Web.Models.Advertisements;
using System;
using System.Collections.Generic;
using System.Linq;
using SecondHand.Web.Infrastructure;
using TestStack.FluentMVCTesting;
using System.Web.Mvc;
using System.Web;
using System.Security.Principal;
using System.IO;
using FakeItEasy;
using System.Web.Routing;
using Kendo.Mvc.UI;

namespace SecondHand.Web.UnitTests.Controllers
{
    [TestFixture]
    public class AdvertisementControllerTests
    {
        [Test]
        public void Constructor_Should_Throw_WhenGivenNullUserService()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new AdvertisementsController(null,
                null,
                null,
                null));
        }

        [Test]
        public void Constructor_Should_Throw_WhenGivenNullAdvertisementsService()
        {
            // Arrange
            var userService = new Mock<IUsersService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new AdvertisementsController(userService.Object,
                null,
                null,
                null));
        }

        [Test]
        public void Constructor_Should_Throw_WhenGivenNullCategoryServiceService()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new AdvertisementsController(userService.Object,
                advertService.Object,
                null,
                null));
        }

        [Test]
        public void Constructor_Should_Throw_WhenGivenNullMapper()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                null));
        }

        [Test]
        public void Constructor_Should_NotThrow_WhenGivenValidDependencies()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            // Act & Assert
            Assert.DoesNotThrow(() => new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object));
        }

        [Test]
        public void Constructor_Should_CreateProperInstance_WhenGivenValidDependencies()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            // Act
            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);

            // Assert
            Assert.IsInstanceOf<AdvertisementsController>(sut);
        }

        [Test]
        public void Index_Should_CallAdvertisementServiceOnce()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            var advertsReturnResult = new List<Advertisement>
            {
                new Advertisement() { Title = "Title" }
            };

            advertService.Setup(x => x.GetAdvertisements(It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<SortType>(),
                It.IsAny<string>()))
                .Returns(advertsReturnResult.AsQueryable());

            advertService.Setup(x => x.LastQueryRecordsCount).Returns(1);

            mapper.Setup(x => x.Map<AdvertisementIndexViewModel>(advertsReturnResult)).Returns(new AdvertisementIndexViewModel { });


            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);

            int pageNumber = 1;
            int pageSize = 1;
            string query = "";
            string sortProperty = "";
            SortType sortType = SortType.Ascending;
            string category = "fashion";

            // Act
            sut.Index(pageNumber, pageSize, query, sortProperty, sortType, category);

            // Assert
            advertService.Verify(x => x.GetAdvertisements(It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<SortType>(),
                It.IsAny<string>()), Times.Once);
        }

        [TestCase(1, 1, "", "Title", SortType.Ascending, "fashion")]
        [TestCase(2, 2, "query", "Description", SortType.Descending, "")]
        [TestCase(3, 4, "asd23r", "", SortType.Ascending, "vehicles")]
        [TestCase(1, 1, "otherQuery", "Id", SortType.Descending, "fashion")]
        public void Index_Should_CallAdvertisementServiceGetAdsWithProperParameters(int pageNumber, int pageSize,
            string query, string sortProperty, SortType sortType, string category)
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            var advertsReturnResult = new List<Advertisement>
            {
                new Advertisement() { Title = "Title" }
            };

            advertService.Setup(x => x.GetAdvertisements(It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<SortType>(),
                It.IsAny<string>()))
                .Returns(advertsReturnResult.AsQueryable());

            advertService.Setup(x => x.LastQueryRecordsCount).Returns(1);

            mapper.Setup(x => x.Map<AdvertisementIndexViewModel>(advertsReturnResult)).Returns(new AdvertisementIndexViewModel { });


            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);

            // Act
            sut.Index(pageNumber, pageSize, query, sortProperty, sortType, category);

            // Assert
            advertService.Verify(x => x.GetAdvertisements(pageNumber,
                pageSize,
                query,
                sortProperty,
                sortType,
                category), Times.Once);
        }

        [TestCase(1, 1, "", "Title", SortType.Ascending, "fashion")]
        public void Index_Should_CallAdvertisementServiceLastQueryCountOnce(int pageNumber, int pageSize,
            string query, string sortProperty, SortType sortType, string category)
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            var advertsReturnResult = new List<Advertisement>
            {
                new Advertisement() { Title = "Title" }
            };

            advertService.Setup(x => x.GetAdvertisements(It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<SortType>(),
                It.IsAny<string>()))
                .Returns(advertsReturnResult.AsQueryable());

            advertService.SetupGet(x => x.LastQueryRecordsCount).Returns(1);

            mapper.Setup(x => x.Map<AdvertisementIndexViewModel>(advertsReturnResult)).Returns(new AdvertisementIndexViewModel { });


            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);

            // Act
            sut.Index(pageNumber, pageSize, query, sortProperty, sortType, category);

            // Assert
            advertService.Verify(x => x.LastQueryRecordsCount, Times.Once());
        }

        [TestCase(1, 1, "", "Title", SortType.Ascending, "fashion")]
        public void Index_Should_ProceedWithTheDefaultView(int pageNumber, int pageSize,
            string query, string sortProperty, SortType sortType, string category)
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            var advertsReturnResult = new List<Advertisement>
            {
                new Advertisement() { Title = "Title" }
            };

            advertService.Setup(x => x.GetAdvertisements(It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<SortType>(),
                It.IsAny<string>()))
                .Returns(advertsReturnResult.AsQueryable());

            advertService.Setup(x => x.LastQueryRecordsCount).Returns(1);

            mapper.Setup(x => x.Map<AdvertisementIndexViewModel>(advertsReturnResult)).Returns(new AdvertisementIndexViewModel { });


            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);

            // Act
            sut.Index(pageNumber, pageSize, query, sortProperty, sortType, category);

            // Assert
            sut.WithCallTo(x => x.Index(pageNumber, pageSize, query, sortProperty, sortType, category))
                .ShouldRenderDefaultView();
        }

        [TestCase(1, 1, "", "Title", SortType.Ascending, "fashion")]
        public void Index_Should_CallDefaultViewWithExpectedModelType(int pageNumber, int pageSize,
            string query, string sortProperty, SortType sortType, string category)
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            var advertsReturnResult = new List<Advertisement>
            {
                new Advertisement() { Title = "Title" }
            };

            var mapperResult = new AdvertisementIndexViewModel() { };

            advertService.Setup(x => x.GetAdvertisements(It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<SortType>(),
                It.IsAny<string>()))
                .Returns(advertsReturnResult.AsQueryable());

            advertService.Setup(x => x.LastQueryRecordsCount).Returns(1);

            mapper.Setup(x => x.Map<AdvertisementIndexViewModel>(advertsReturnResult)).Returns(mapperResult);


            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);

            // Act
            sut.Index(pageNumber, pageSize, query, sortProperty, sortType, category);

            // Assert
            sut.WithCallTo(x => x.Index(pageNumber, pageSize, query, sortProperty, sortType, category))
                .ShouldRenderDefaultView().WithModel<AdvertisementIndexViewModel>();
        }

        [TestCase(1, 1, "", "Title", SortType.Ascending, "fashion")]
        public void Index_Should_ReturnActionResult(int pageNumber, int pageSize,
            string query, string sortProperty, SortType sortType, string category)
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            var advertsReturnResult = new List<Advertisement>
            {
                new Advertisement() { Title = "Title" }
            };

            var mapperResult = new AdvertisementIndexViewModel() { };

            advertService.Setup(x => x.GetAdvertisements(It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<SortType>(),
                It.IsAny<string>()))
                .Returns(advertsReturnResult.AsQueryable());

            advertService.Setup(x => x.LastQueryRecordsCount).Returns(1);

            mapper.Setup(x => x.Map<AdvertisementIndexViewModel>(advertsReturnResult)).Returns(mapperResult);


            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);

            // Act
            var result = sut.Index(pageNumber, pageSize, query, sortProperty, sortType, category);

            // Assert
            Assert.IsInstanceOf<ActionResult>(result);
        }

        [Test]
        public void AddAdvertisement_Should_CallCategoryServiceOnce()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            var categoriesReturnResult = new List<Category>
            {
                new Category() { Name = "Category" }
            };

            categoryService.Setup(x => x.GetAll()).Returns(categoriesReturnResult.AsQueryable());

            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);

            // Act
            sut.AddAdvertisement();

            // Assert
            categoryService.Verify(x => x.GetAll(), Times.Once);
        }

        [Test]
        public void AddAdvertisement_Should_UseTheDefaultView()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            var categoriesReturnResult = new List<Category>
            {
                new Category() { Name = "Category" }
            };

            categoryService.Setup(x => x.GetAll()).Returns(categoriesReturnResult.AsQueryable());

            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);

            // Act
            sut.AddAdvertisement();

            // Assert
            sut.WithCallTo(x => x.AddAdvertisement())
                .ShouldRenderDefaultView();
        }

        [Test]
        public void AddAdvertisement_Should_UseTheDefaultViewWithProperModel()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            var categoriesReturnResult = new List<Category>
            {
                new Category() { Name = "Category" }
            };

            categoryService.Setup(x => x.GetAll()).Returns(categoriesReturnResult.AsQueryable());

            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);

            // Act
            sut.AddAdvertisement();

            // Assert
            sut.WithCallTo(x => x.AddAdvertisement())
                .ShouldRenderDefaultView()
                .WithModel<AdvertisementCreationViewModel>(x => x.Categories.FirstOrDefault().Text == "Category");
        }

        [Test]
        public void AddAdvertisementPost_Should_ReturnDefaultViewWhenGivenInvalidModel()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            var categoriesReturnResult = new List<Category>
            {
                new Category() { Name = "Category" }
            };

            categoryService.Setup(x => x.GetAll()).Returns(categoriesReturnResult.AsQueryable());

            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);

            sut.ModelState.AddModelError("Key", "Value");

            // Act
            sut.AddAdvertisement(new AdvertisementCreationViewModel { Title = "" });

            // Assert
            sut.WithCallTo(x => x.AddAdvertisement(It.IsAny<AdvertisementCreationViewModel>()))
                .ShouldRenderDefaultView();
        }

        [Test]
        public void AddAdvertisementPost_ShouldCallMapperMapOnceWithProperModel()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            var categoriesReturnResult = new List<Category>
            {
                new Category() { Name = "Category" }
            };

            var viewModel = new AdvertisementCreationViewModel { Title = "", Category = "Valid" };
            var dbModel = new Advertisement { };

            categoryService.Setup(x => x.GetAll()).Returns(categoriesReturnResult.AsQueryable());
            mapper.Setup(x => x.Map<Advertisement>(viewModel)).Returns(dbModel);
            userService.Setup(x => x.GetByUsername(It.IsAny<string>())).Returns(new ApplicationUser { });
            advertService.Setup(x => x.CreateAdvertisement(dbModel, "Category"));

            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);

            var httpContextBaseMock = new Mock<HttpContextBase>();
            httpContextBaseMock.Setup(x => x.User.Identity.Name).Returns("username");
            sut.ControllerContext = new ControllerContext(httpContextBaseMock.Object, new RouteData(), sut);

            // Act
            sut.AddAdvertisement(viewModel);

            // Assert
            mapper.Verify(x => x.Map<Advertisement>(viewModel), Times.Once);
        }

        [Test]
        public void AddAdvertisementPost_ShouldCallUserServiceGetByUsernameOnceWithProperParams()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            var categoriesReturnResult = new List<Category>
            {
                new Category() { Name = "Category" }
            };

            var viewModel = new AdvertisementCreationViewModel { Title = "", Category = "Valid" };
            var dbModel = new Advertisement { };
            var expectedUsername = "username";

            categoryService.Setup(x => x.GetAll()).Returns(categoriesReturnResult.AsQueryable());
            mapper.Setup(x => x.Map<Advertisement>(viewModel)).Returns(dbModel);
            userService.Setup(x => x.GetByUsername(It.IsAny<string>())).Returns(new ApplicationUser { });
            advertService.Setup(x => x.CreateAdvertisement(dbModel, "Category"));

            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);


            var httpContextBaseMock = new Mock<HttpContextBase>();
            httpContextBaseMock.Setup(x => x.User.Identity.Name).Returns(expectedUsername);
            sut.ControllerContext = new ControllerContext(httpContextBaseMock.Object, new RouteData(), sut);


            // Act
            sut.AddAdvertisement(viewModel);

            // Assert
            userService.Verify(x => x.GetByUsername(expectedUsername), Times.Once);
        }

        [Test]
        public void AddAdvertisementPost_ShouldRedirectToIndexAction_When_NoErrors()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            var categoriesReturnResult = new List<Category>
            {
                new Category() { Name = "Category" }
            };

            var viewModel = new AdvertisementCreationViewModel { Title = "", Category = "Valid" };
            var dbModel = new Advertisement { };
            var expectedUsername = "username";

            categoryService.Setup(x => x.GetAll()).Returns(categoriesReturnResult.AsQueryable());
            mapper.Setup(x => x.Map<Advertisement>(viewModel)).Returns(dbModel);
            userService.Setup(x => x.GetByUsername(It.IsAny<string>())).Returns(new ApplicationUser { });
            advertService.Setup(x => x.CreateAdvertisement(dbModel, "Category"));

            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);


            var httpContextBaseMock = new Mock<HttpContextBase>();
            httpContextBaseMock.Setup(x => x.User.Identity.Name).Returns(expectedUsername);
            sut.ControllerContext = new ControllerContext(httpContextBaseMock.Object, new RouteData(), sut);


            // Act
            var result = (RedirectToRouteResult)sut.AddAdvertisement(viewModel);

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [Test]
        public void Details_Should_CallAdvertisementServiceOnceWithGivenId()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            var id = new Guid();
            var dbModel = new Advertisement { };
            var viewModel = new AdvertisementDetailsViewModel { };

            advertService.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(dbModel);
            mapper.Setup(x => x.Map<AdvertisementDetailsViewModel>(dbModel)).Returns(viewModel);

            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);

            // Act
            sut.Details(id);

            // Assert
            advertService.Verify(x => x.GetById(id), Times.Once);
        }

        [Test]
        public void Details_Should_CallMapperOnceWithTheFoundDbModel()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            var id = new Guid();
            var dbModel = new Advertisement { };
            var viewModel = new AdvertisementDetailsViewModel { };

            advertService.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(dbModel);
            mapper.Setup(x => x.Map<AdvertisementDetailsViewModel>(dbModel)).Returns(viewModel);

            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);

            // Act
            sut.Details(id);

            // Assert
            mapper.Verify(x => x.Map<AdvertisementDetailsViewModel>(dbModel), Times.Once);
        }

        [Test]
        public void Details_Should_RedirectToIndexActionIfAdvertisementIsNotFound()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            var id = new Guid();
            Advertisement dbModel = null;
            var viewModel = new AdvertisementDetailsViewModel { };

            advertService.Setup(x => x.GetById(It.IsAny<Guid>())).Returns<AdvertisementDetailsViewModel>(null);

            mapper.Setup(x => x.Map<AdvertisementDetailsViewModel>(dbModel)).Returns(viewModel);

            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);

            // Act
            var result = (RedirectToRouteResult)sut.Details(id);

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [Test]
        public void Details_Should_CallDefaultViewWithProperModelfNoErrors()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            var id = new Guid();
            var dbModel = new Advertisement { };
            var viewModel = new AdvertisementDetailsViewModel { };

            advertService.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(dbModel);

            mapper.Setup(x => x.Map<AdvertisementDetailsViewModel>(dbModel)).Returns(viewModel);

            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);

            // Act
            sut.Details(id);

            // Assert
            sut.WithCallTo(x => x.Details(id))
                .ShouldRenderDefaultView()
                .WithModel<AdvertisementDetailsViewModel>(viewModel);
        }

        [Test]
        public void MyAdvertisements_Should_CallDefaultView()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);

            var httpContextBaseMock = new Mock<HttpContextBase>();
            httpContextBaseMock.Setup(x => x.User.Identity.Name).Returns("gosho");
            sut.ControllerContext = new ControllerContext(httpContextBaseMock.Object, new RouteData(), sut);

            // Act
            sut.MyAdvertisements();


            // Assert
            sut.WithCallTo(x => x.MyAdvertisements())
                .ShouldRenderDefaultView();
        }

        [Test]
        public void Delete_Should_CallAdvertServiceGetByIdOnceWithTheGivenId()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            var id = new Guid();
            string loggedUserName = "user";
            var dbModel = new Advertisement
            {
                AddedBy = new ApplicationUser
                {
                    UserName = loggedUserName
                }
            };

            advertService.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(dbModel);
            advertService.Setup(x => x.Remove(dbModel));

            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);

            var httpContextBaseMock = new Mock<HttpContextBase>();
            httpContextBaseMock.Setup(x => x.User.Identity.Name).Returns(loggedUserName);
            sut.ControllerContext = new ControllerContext(httpContextBaseMock.Object, new RouteData(), sut);

            // Act
            sut.Delete(id);


            // Assert
            advertService.Verify(x => x.GetById(id), Times.Once);
        }

        [Test]
        public void Delete_Should_RedirectToIndexActionIfNoAdFound()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            var id = new Guid();
            string loggedUserName = "user";
            var dbModel = new Advertisement
            {
                AddedBy = new ApplicationUser
                {
                    UserName = loggedUserName
                }
            };

            advertService.Setup(x => x.GetById(It.IsAny<Guid>())).Returns<Advertisement>(null);
            advertService.Setup(x => x.Remove(dbModel));

            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);

            var httpContextBaseMock = new Mock<HttpContextBase>();
            httpContextBaseMock.Setup(x => x.User.Identity.Name).Returns(loggedUserName);
            sut.ControllerContext = new ControllerContext(httpContextBaseMock.Object, new RouteData(), sut);

            // Act
            var result = (RedirectToRouteResult)sut.Delete(id);


            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [Test]
        public void Delete_Should_RedirectToDetailsIfLoggedUserIsNotAdOwner()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            var id = new Guid();
            string loggedUserName = "owner";

            var dbModel = new Advertisement
            {
                AddedBy = new ApplicationUser
                {
                    UserName = "owner"
                }
            };

            advertService.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(dbModel);
            advertService.Setup(x => x.Remove(dbModel));

            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);

            var httpContextBaseMock = new Mock<HttpContextBase>();
            httpContextBaseMock.Setup(x => x.User.Identity.Name).Returns("not owner");
            sut.ControllerContext = new ControllerContext(httpContextBaseMock.Object, new RouteData(), sut);

            // Act
            var result = (RedirectToRouteResult)sut.Delete(id);


            // Assert
            Assert.AreEqual("Details", result.RouteValues["action"]);
        }

        [Test]
        public void Delete_Should_CallAdvertServiceRemoveIfUserIsOwner()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            var id = new Guid();
            string loggedUserName = "user";

            var dbModel = new Advertisement
            {
                AddedBy = new ApplicationUser
                {
                    UserName = loggedUserName
                }
            };

            advertService.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(dbModel);
            advertService.Setup(x => x.Remove(dbModel));

            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);

            var httpContextBaseMock = new Mock<HttpContextBase>();
            httpContextBaseMock.Setup(x => x.User.Identity.Name).Returns(loggedUserName);
            sut.ControllerContext = new ControllerContext(httpContextBaseMock.Object, new RouteData(), sut);

            // Act
            sut.Delete(id);


            // Assert
            advertService.Verify(x => x.Remove(id), Times.Once);
        }

        [Test]
        public void Delete_Should_RedirectToMyAdvertisementsActionIfNoErrors()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            var id = new Guid();
            string loggedUserName = "user";

            var dbModel = new Advertisement
            {
                AddedBy = new ApplicationUser
                {
                    UserName = loggedUserName
                }
            };

            advertService.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(dbModel);
            advertService.Setup(x => x.Remove(dbModel));

            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);

            var httpContextBaseMock = new Mock<HttpContextBase>();
            httpContextBaseMock.Setup(x => x.User.Identity.Name).Returns(loggedUserName);
            sut.ControllerContext = new ControllerContext(httpContextBaseMock.Object, new RouteData(), sut);

            // Act
            var result = (RedirectToRouteResult)sut.Delete(id);


            // Assert
            Assert.AreEqual("MyAdvertisements", result.RouteValues["action"]);
        }

        //////// 
        [Test]
        public void Edit_Should_CallAdvertServiceGetByIdOnceWithTheGivenId()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            var id = new Guid();
            string loggedUserName = "user";
            var dbModel = new Advertisement
            {
                AddedBy = new ApplicationUser
                {
                    UserName = loggedUserName
                }
            };

            advertService.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(dbModel);
            advertService.Setup(x => x.Remove(dbModel));

            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);

            var httpContextBaseMock = new Mock<HttpContextBase>();
            httpContextBaseMock.Setup(x => x.User.Identity.Name).Returns(loggedUserName);
            sut.ControllerContext = new ControllerContext(httpContextBaseMock.Object, new RouteData(), sut);

            // Act
            sut.Edit(id);


            // Assert
            advertService.Verify(x => x.GetById(id), Times.Once);
        }

        [Test]
        public void Edit_Should_RedirectToIndexActionIfNoAdFound()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            var id = new Guid();
            string loggedUserName = "user";
            var dbModel = new Advertisement
            {
                AddedBy = new ApplicationUser
                {
                    UserName = loggedUserName
                }
            };

            advertService.Setup(x => x.GetById(It.IsAny<Guid>())).Returns<Advertisement>(null);
            advertService.Setup(x => x.Remove(dbModel));

            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);

            var httpContextBaseMock = new Mock<HttpContextBase>();
            httpContextBaseMock.Setup(x => x.User.Identity.Name).Returns(loggedUserName);
            sut.ControllerContext = new ControllerContext(httpContextBaseMock.Object, new RouteData(), sut);

            // Act
            var result = (RedirectToRouteResult)sut.Edit(id);


            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [Test]
        public void Edit_Should_RedirectToDetailsIfLoggedUserIsNotAdOwner()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            var id = new Guid();
            string loggedUserName = "owner";

            var dbModel = new Advertisement
            {
                AddedBy = new ApplicationUser
                {
                    UserName = "owner"
                }
            };

            advertService.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(dbModel);
            advertService.Setup(x => x.Remove(dbModel));

            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);

            var httpContextBaseMock = new Mock<HttpContextBase>();
            httpContextBaseMock.Setup(x => x.User.Identity.Name).Returns("not owner");
            sut.ControllerContext = new ControllerContext(httpContextBaseMock.Object, new RouteData(), sut);

            // Act
            var result = (RedirectToRouteResult)sut.Edit(id);


            // Assert
            Assert.AreEqual("Details", result.RouteValues["action"]);
        }

        [Test]
        public void Edit_Should_CallMapperOnceWithTheDbModel()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            var id = new Guid();
            string loggedUserName = "user";

            var dbModel = new Advertisement
            {
                AddedBy = new ApplicationUser
                {
                    UserName = loggedUserName
                }
            };

            var viewModel = new AdvertisementEditViewModel { };

            mapper.Setup(x => x.Map<AdvertisementEditViewModel>(dbModel)).Returns(viewModel);

            advertService.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(dbModel);
            advertService.Setup(x => x.Remove(dbModel));

            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);

            var httpContextBaseMock = new Mock<HttpContextBase>();
            httpContextBaseMock.Setup(x => x.User.Identity.Name).Returns(loggedUserName);
            sut.ControllerContext = new ControllerContext(httpContextBaseMock.Object, new RouteData(), sut);

            // Act
            sut.Edit(id);


            // Assert
            mapper.Verify(x => x.Map<AdvertisementEditViewModel>(dbModel), Times.Once);
        }

        [Test]
        public void Edit_Should_ReturnTheDefaultViewWithTheViewModel()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            var id = new Guid();
            string loggedUserName = "user";

            var dbModel = new Advertisement
            {
                AddedBy = new ApplicationUser
                {
                    UserName = loggedUserName
                }
            };

            var viewModel = new AdvertisementEditViewModel { };

            mapper.Setup(x => x.Map<AdvertisementEditViewModel>(dbModel)).Returns(viewModel);

            advertService.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(dbModel);
            advertService.Setup(x => x.Remove(dbModel));

            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);

            var httpContextBaseMock = new Mock<HttpContextBase>();
            httpContextBaseMock.Setup(x => x.User.Identity.Name).Returns(loggedUserName);
            sut.ControllerContext = new ControllerContext(httpContextBaseMock.Object, new RouteData(), sut);

            // Act
            sut.Edit(id);


            // Assert
            sut.WithCallTo(x => x.Edit(id))
                .ShouldRenderDefaultView()
                .WithModel(viewModel);
        }

        [Test]
        public void EditPost_Should_ReturnDefaultViewIfModelStateNotValid()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            var id = new Guid();
            string loggedUserName = "user";

            var dbModel = new Advertisement
            {
                AddedBy = new ApplicationUser
                {
                    UserName = loggedUserName
                }
            };

            var viewModel = new AdvertisementEditViewModel { };

            mapper.Setup(x => x.Map<AdvertisementEditViewModel>(dbModel)).Returns(viewModel);

            advertService.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(dbModel);
            advertService.Setup(x => x.Remove(dbModel));

            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);

            sut.ModelState.AddModelError("Key", "Value");

            var httpContextBaseMock = new Mock<HttpContextBase>();
            httpContextBaseMock.Setup(x => x.User.Identity.Name).Returns(loggedUserName);
            sut.ControllerContext = new ControllerContext(httpContextBaseMock.Object, new RouteData(), sut);

            // Act
            sut.Edit(viewModel);


            // Assert
            sut.WithCallTo(x => x.Edit(viewModel))
                .ShouldRenderDefaultView();
        }

        [Test]
        public void EditPost_Should_CallAdvertServiceGetByIdWithTheGivenModelId()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            var id = new Guid();
            string loggedUserName = "user";

            var dbModel = new Advertisement
            {
                AddedBy = new ApplicationUser
                {
                    UserName = loggedUserName
                }
            };

            var viewModel = new AdvertisementEditViewModel
            {
                Id = id
            };

            mapper.Setup(x => x.Map<AdvertisementEditViewModel>(dbModel)).Returns(viewModel);

            advertService.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(dbModel);
            advertService.Setup(x => x.Remove(dbModel));

            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);

            var httpContextBaseMock = new Mock<HttpContextBase>();
            httpContextBaseMock.Setup(x => x.User.Identity.Name).Returns(loggedUserName);
            sut.ControllerContext = new ControllerContext(httpContextBaseMock.Object, new RouteData(), sut);

            // Act
            sut.Edit(viewModel);


            // Assert
            advertService.Verify(x => x.GetById(id), Times.Once);
        }

        [Test]
        public void EditPost_Should_RedirectToIndexActionIfNoDbModelFound()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            var id = new Guid();
            string loggedUserName = "user";

            var dbModel = new Advertisement
            {
                AddedBy = new ApplicationUser
                {
                    UserName = loggedUserName
                }
            };

            var viewModel = new AdvertisementEditViewModel
            {
                Id = id
            };

            mapper.Setup(x => x.Map<AdvertisementEditViewModel>(dbModel)).Returns(viewModel);

            advertService.Setup(x => x.GetById(It.IsAny<Guid>())).Returns<Advertisement>(null);
            advertService.Setup(x => x.Remove(dbModel));

            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);

            var httpContextBaseMock = new Mock<HttpContextBase>();
            httpContextBaseMock.Setup(x => x.User.Identity.Name).Returns(loggedUserName);
            sut.ControllerContext = new ControllerContext(httpContextBaseMock.Object, new RouteData(), sut);

            // Act
            var result = (RedirectToRouteResult)sut.Edit(viewModel);


            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [Test]
        public void EditPost_Should_RedirectToDetailsActionIfLoggedUserIsNotOwner()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            var id = new Guid();

            var dbModel = new Advertisement
            {
                AddedBy = new ApplicationUser
                {
                    UserName = "owner"
                }
            };

            var viewModel = new AdvertisementEditViewModel
            {
                Id = id,
            };

            mapper.Setup(x => x.Map<AdvertisementEditViewModel>(dbModel)).Returns(viewModel);

            advertService.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(dbModel);
            advertService.Setup(x => x.Remove(dbModel));

            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);

            var httpContextBaseMock = new Mock<HttpContextBase>();
            httpContextBaseMock.Setup(x => x.User.Identity.Name).Returns("not owner");
            sut.ControllerContext = new ControllerContext(httpContextBaseMock.Object, new RouteData(), sut);

            // Act
            var result = (RedirectToRouteResult)sut.Edit(viewModel);


            // Assert
            Assert.AreEqual("Details", result.RouteValues["action"]);
        }

        [Test]
        public void EditPost_Should_CallAdvertServiceEditIfNoErrorsWithTheDbModel()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            var id = new Guid();
            string loggedUsername = "user";

            var dbModel = new Advertisement
            {
                AddedBy = new ApplicationUser
                {
                    UserName = loggedUsername
                }
            };

            var viewModel = new AdvertisementEditViewModel
            {
                Id = id,
            };

            mapper.Setup(x => x.Map<AdvertisementEditViewModel>(dbModel)).Returns(viewModel);

            advertService.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(dbModel);
            advertService.Setup(x => x.Remove(dbModel));
            advertService.Setup(x => x.Edit(dbModel));

            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);

            var httpContextBaseMock = new Mock<HttpContextBase>();
            httpContextBaseMock.Setup(x => x.User.Identity.Name).Returns(loggedUsername);
            sut.ControllerContext = new ControllerContext(httpContextBaseMock.Object, new RouteData(), sut);

            // Act
            sut.Edit(viewModel);


            // Assert
            advertService.Verify(x => x.Edit(dbModel), Times.Once);
        }

        [Test]
        public void EditPost_Should_RedirectToDetailsActionIfNoErrors()
        {
            // Arrange
            var userService = new Mock<IUsersService>();
            var advertService = new Mock<IAdvertisementsService>();
            var categoryService = new Mock<ICategoryService>();
            var mapper = new Mock<IMapper>();

            var id = new Guid();
            string loggedUsername = "user";

            var dbModel = new Advertisement
            {
                AddedBy = new ApplicationUser
                {
                    UserName = loggedUsername
                }
            };

            var viewModel = new AdvertisementEditViewModel
            {
                Id = id,
            };

            mapper.Setup(x => x.Map<AdvertisementEditViewModel>(dbModel)).Returns(viewModel);

            advertService.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(dbModel);
            advertService.Setup(x => x.Remove(dbModel));
            advertService.Setup(x => x.Edit(dbModel));

            var sut = new AdvertisementsController(userService.Object,
                advertService.Object,
                categoryService.Object,
                mapper.Object);

            var httpContextBaseMock = new Mock<HttpContextBase>();
            httpContextBaseMock.Setup(x => x.User.Identity.Name).Returns(loggedUsername);
            sut.ControllerContext = new ControllerContext(httpContextBaseMock.Object, new RouteData(), sut);

            // Act
            var result = (RedirectToRouteResult)sut.Edit(viewModel);


            // Assert
            Assert.AreEqual("Details", result.RouteValues["action"]);
        }

        //[Test]
        //public void UserAdvertisements_Should_CallAdvertServiceGetUserAdsOnceWithCalledUsername()
        //{
        //    // Arrange
        //    var userService = new Mock<IUsersService>();
        //    var advertService = new Mock<IAdvertisementsService>();
        //    var categoryService = new Mock<ICategoryService>();
        //    var mapper = new Mock<IMapper>();

        //    var mapperReturnResult = new AdvertisementListItemViewModel
        //    {
        //        AddedById = "string-id",
        //        AdderUsername = "user",
        //        CreatedOn = DateTime.Now,
        //        CurrencyType = CurrencyType.USD,
        //        Id = new Guid(),
        //        Price = 5m,
        //        PrimaryImageUrl = "http://img.com/"
        //    };

        //    var serviceReturnResult = new List<Advertisement>()
        //    {
        //        new Advertisement { }
        //    };

        //    advertService.Setup(x => x.GetUserAdvertisements(It.IsAny<string>())).Returns(serviceReturnResult.AsQueryable());
        //    mapper.Setup(x => x.Map<AdvertisementListItemViewModel>(serviceReturnResult)).Returns(mapperReturnResult);
            
        //    var sut = new AdvertisementsController(userService.Object,
        //        advertService.Object,
        //        categoryService.Object,
        //        mapper.Object);

        //    var dataSrcRequest = new DataSourceRequest();

        //    // Act
        //    sut.UserAdvertisements(dataSrcRequest, "username");


        //    // Assert
        //    advertService.Verify(x => x.GetUserAdvertisements("username"), Times.Once);
        //}
    }
}
