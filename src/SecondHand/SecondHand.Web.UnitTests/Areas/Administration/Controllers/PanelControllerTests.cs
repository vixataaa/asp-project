using NUnit.Framework;
using SecondHand.Web.Areas.Administration.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;

namespace SecondHand.Web.UnitTests.Areas.Administration.Controllers
{
    [TestFixture]
    public class PanelControllerTests
    {
        [Test]
        public void Index_Should_ReturnTheDefaultView()
        {
            // Arrange
            var sut = new PanelController();

            // Act
            sut.Index();

            // Assert
            sut.WithCallTo(x => x.Index())
                .ShouldRenderDefaultView();
        }
    }
}
