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
    public class CategoryServiceTests
    {
        [Test]
        public void Constructor_Should_Throw_WhenGivenNullCategoryRepo()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new CategoryService(null));
        }

        [Test]
        public void Constructor_Should_NotThrow_WhenGivenValidCategoryRepo()
        {
            // Arrange
            var catRepo = new Mock<ICategoryRepository>();
            // Act & Assert
            Assert.DoesNotThrow(() => new CategoryService(catRepo.Object));
        }


        [Test]
        public void GetAll_Should_CallCategoryRepoAllAndReturnIt()
        {
            // Arrange
            var catRepo = new Mock<ICategoryRepository>();

            var expected = new List<Category>().AsQueryable();
            catRepo.Setup(x => x.All).Returns(expected);

            var sut = new CategoryService(catRepo.Object);

            // Act
            var result = sut.GetAll();

            // Assert
            catRepo.Verify(x => x.All, Times.Once);
            Assert.AreEqual(expected, result);
        }
    }
}
