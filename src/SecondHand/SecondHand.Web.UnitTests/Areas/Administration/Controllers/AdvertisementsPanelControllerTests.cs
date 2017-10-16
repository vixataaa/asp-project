using AutoMapper;
using Kendo.Mvc.UI;
using Moq;
using NUnit.Framework;
using SecondHand.Data.Models;
using SecondHand.Services.Data.Contracts;
using SecondHand.Web.Areas.Administration.Controllers;
using SecondHand.Web.Areas.Administration.Models.AdvertisementsPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace SecondHand.Web.UnitTests.Areas.Administration.Controllers
{
    [TestFixture]
    public class AdvertisementsPanelControllerTests
    {
        [Test]
        public void Constructor_Should_Throw_WhenGivenNullAdvertsService()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new AdvertisementsPanelController(null, null));
        }

        [Test]
        public void Constructor_Should_Throw_WhenGivenNullMapper()
        {
            // Arrange
            var advertsService = new Mock<IAdvertisementsService>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new AdvertisementsPanelController(advertsService.Object, null));
        }

        [Test]
        public void Constructor_ShouldNot_Throw_WhenGivenValidDependencies()
        {
            // Arrange
            var advertsService = new Mock<IAdvertisementsService>();
            var mapper = new Mock<IMapper>();

            // Act & Assert
            Assert.DoesNotThrow(() => new AdvertisementsPanelController(advertsService.Object, mapper.Object));
        }

        [Test]
        public void Index_Should_ReturnDefaultView()
        {
            // Arrange
            var advertsService = new Mock<IAdvertisementsService>();
            var mapper = new Mock<IMapper>();

            var sut = new AdvertisementsPanelController(advertsService.Object, mapper.Object);

            // Act
            sut.Index();

            // Assert
            sut.WithCallTo(x => x.Index())
                .ShouldRenderDefaultView();
        }

        [Test]
        public void GetAdvertisements_Should_CallAdvertServiceGetAdvertsOnce()
        {
            // Arrange
            var advertsService = new Mock<IAdvertisementsService>();
            var mapper = new Mock<IMapper>();            

            var getAdvsResult = new List<Advertisement>()
            {
                new Advertisement { }
            };

            var mapAdvsResult = new AdvertisementGridViewModel { };

            advertsService.Setup(x => x.GetAdvertisements()).Returns(getAdvsResult.AsQueryable());
            mapper.Setup(x => x.Map<AdvertisementGridViewModel>(getAdvsResult)).Returns(mapAdvsResult);
            var request = new DataSourceRequest();

            var sut = new AdvertisementsPanelController(advertsService.Object, mapper.Object);

            // Act
            sut.GetAdvertisements(request);

            // Assert
            advertsService.Verify(x => x.GetAdvertisements(), Times.Once);
        }

        [Test]
        public void GetAdvertisements_Should_ReturnJsonResult()
        {
            // Arrange
            var advertsService = new Mock<IAdvertisementsService>();
            var mapper = new Mock<IMapper>();

            var getAdvsResult = new List<Advertisement>()
            {
                new Advertisement { }
            };

            var mapAdvsResult = new AdvertisementGridViewModel { };

            advertsService.Setup(x => x.GetAdvertisements()).Returns(getAdvsResult.AsQueryable());
            mapper.Setup(x => x.Map<AdvertisementGridViewModel>(getAdvsResult)).Returns(mapAdvsResult);
            var request = new DataSourceRequest();

            var sut = new AdvertisementsPanelController(advertsService.Object, mapper.Object);

            // Act
            sut.GetAdvertisements(request);

            // Assert
            sut.WithCallTo(x => x.GetAdvertisements(request))
                .ShouldReturnJson();
        }

        [Test]
        public void EditAdvertisement_Should_CallMapperWithThePassedModelOnce()
        {
            // Arrange
            var advertsService = new Mock<IAdvertisementsService>();
            var mapper = new Mock<IMapper>();

            var dbModel = new Advertisement { };

            var viewModel = new AdvertisementGridViewModel { };

            advertsService.Setup(x => x.Edit(dbModel));
            mapper.Setup(x => x.Map<Advertisement>(viewModel)).Returns(dbModel);

            var request = new DataSourceRequest();

            var sut = new AdvertisementsPanelController(advertsService.Object, mapper.Object);

            // Act
            sut.EditAdvertisement(viewModel);

            // Assert
            mapper.Verify(x => x.Map<Advertisement>(viewModel), Times.Once);
        }

        [Test]
        public void EditAdvertisement_Should_AdvertsServiceEditWithTheDbModelOnce()
        {
            // Arrange
            var advertsService = new Mock<IAdvertisementsService>();
            var mapper = new Mock<IMapper>();

            var dbModel = new Advertisement { };

            var viewModel = new AdvertisementGridViewModel { };

            advertsService.Setup(x => x.Edit(dbModel));
            mapper.Setup(x => x.Map<Advertisement>(viewModel)).Returns(dbModel);

            var request = new DataSourceRequest();

            var sut = new AdvertisementsPanelController(advertsService.Object, mapper.Object);

            // Act
            sut.EditAdvertisement(viewModel);

            // Assert
            advertsService.Verify(x => x.Edit(dbModel), Times.Once);
        }


        [Test]
        public void EditAdvertisement_Should_ReturnJsonResult()
        {
            // Arrange
            var advertsService = new Mock<IAdvertisementsService>();
            var mapper = new Mock<IMapper>();

            var dbModel = new Advertisement { };

            var viewModel = new AdvertisementGridViewModel { };

            advertsService.Setup(x => x.Edit(dbModel));
            mapper.Setup(x => x.Map<Advertisement>(viewModel)).Returns(dbModel);

            var request = new DataSourceRequest();

            var sut = new AdvertisementsPanelController(advertsService.Object, mapper.Object);

            // Act
            sut.EditAdvertisement(viewModel);

            // Assert
            sut.WithCallTo(x => x.EditAdvertisement(viewModel))
                .ShouldReturnJson();
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void RemoveAdvertisement_Should_CallMapperWithThePassedModelOnce()
        {
            // Arrange
            var advertsService = new Mock<IAdvertisementsService>();
            var mapper = new Mock<IMapper>();

            var dbModel = new Advertisement { };

            var viewModel = new AdvertisementGridViewModel { };

            advertsService.Setup(x => x.Edit(dbModel));
            mapper.Setup(x => x.Map<Advertisement>(viewModel)).Returns(dbModel);

            var request = new DataSourceRequest();

            var sut = new AdvertisementsPanelController(advertsService.Object, mapper.Object);

            // Act
            sut.RemoveAdvertisement(viewModel);

            // Assert
            mapper.Verify(x => x.Map<Advertisement>(viewModel), Times.Once);
        }

        [Test]
        public void RemovetAdvertisement_Should_AdvertsServiceRemoveWithTheDbModelOnce()
        {
            // Arrange
            var advertsService = new Mock<IAdvertisementsService>();
            var mapper = new Mock<IMapper>();

            var dbModel = new Advertisement { };

            var viewModel = new AdvertisementGridViewModel { };

            advertsService.Setup(x => x.Remove(dbModel));
            mapper.Setup(x => x.Map<Advertisement>(viewModel)).Returns(dbModel);

            var request = new DataSourceRequest();

            var sut = new AdvertisementsPanelController(advertsService.Object, mapper.Object);

            // Act
            sut.RemoveAdvertisement(viewModel);

            // Assert
            advertsService.Verify(x => x.Remove(dbModel), Times.Once);
        }


        [Test]
        public void RemoveAdvertisement_Should_ReturnJsonResult()
        {
            // Arrange
            var advertsService = new Mock<IAdvertisementsService>();
            var mapper = new Mock<IMapper>();

            var dbModel = new Advertisement { };

            var viewModel = new AdvertisementGridViewModel { };

            advertsService.Setup(x => x.Edit(dbModel));
            mapper.Setup(x => x.Map<Advertisement>(viewModel)).Returns(dbModel);

            var request = new DataSourceRequest();

            var sut = new AdvertisementsPanelController(advertsService.Object, mapper.Object);

            // Act
            sut.RemoveAdvertisement(viewModel);

            // Assert
            sut.WithCallTo(x => x.RemoveAdvertisement(viewModel))
                .ShouldReturnJson();
        }
    }
}
