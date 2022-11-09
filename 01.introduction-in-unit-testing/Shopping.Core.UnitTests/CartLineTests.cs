using System;
using FluentAssertions;
using Xunit;

namespace Shopping.Core.UnitTests
{
    public class CartLineTests : IDisposable
    {
        private readonly CartLineProduct _product;
        private readonly CartLine _systemUnderTest;

        // Setup
        public CartLineTests()
        {
            _product = new CartLineProduct("101", "Mouse", new Price(57.50));
            _systemUnderTest = new CartLine(_product);
        }

        // Running test
        [Fact]
        public void When_NewCartLineIsCreated_Should_HaveUniqueIds()
        {
            // Act
            var result = new CartLine(_product);
            var result2 = new CartLine(_product);

            // Assert
            result.Id.Should().NotBe(result2.Id);
        }

        [Fact]
        public void When_NewCartLineIsCreated_Should_HaveTotalPriceOfProduct()
        {
            // Act
            var result = _systemUnderTest.TotalPrice;

            // Assert
            result.Should().Be(_product.Price);
        }

        [Fact]
        public void When_ChangingQuantityWithNegativeValue_Should_ThrowException()
        {
            // Act
            Action result = () => _systemUnderTest.ChangeQuantity(-1);

            // Assert
            result.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void When_ChangingQuantityWithPositiveValue_Should_HaveUpdatedQuantity()
        {
            // Arrange
            var expectedQuantity = 5;

            // Act
            _systemUnderTest.ChangeQuantity(expectedQuantity);

            // Arrange
            _systemUnderTest.Quantity.Should().Be(expectedQuantity);
        }

        [Fact]
        public void When_ChangingQuantityWithPositiveValue_Should_HaveUpdatedTotalPrice()
        {
            // Arrange
            var expectedPrice = new Price(287.5);

            // Act
            _systemUnderTest.ChangeQuantity(5);

            // Arrange
            _systemUnderTest.TotalPrice.Should().Be(expectedPrice);
        }

        // Teardown
        public void Dispose()
        {
            // nothing to do here
        }
    }
}
