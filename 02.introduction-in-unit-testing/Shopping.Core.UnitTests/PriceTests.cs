using System;
using Xunit;

namespace Shopping.Core.UnitTests
{
    public class PriceTests
    {
        [Fact]
        public void When_NewPriceIsCreated_Should_HaveCorrectAmount()
        {
            // Act
            var systemUnderTest = new Price(9.99);

            // Assert
            Assert.Equal(9.99, systemUnderTest.Amount);
        }

        [Fact]
        public void When_NewPriceIsCreatedWithZeroAmount_Should_ThrowException()
        {
            // Act
            Action result = () => new Price(0);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(result);
        }

        [Fact]
        public void When_NewPriceIsCreatedWithNegativeAmount_Should_ThrowException()
        {
            // Act
            Action result = () => new Price(-0.001);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(result);
        }

        [Fact]
        public void When_NewPriceIsCreated_Should_HaveCorrectFormat()
        {
            // Arrange
            var systemUnderTest = new Price(9.99);

            // Act
            var result = systemUnderTest.ToString();

            // Assert
            Assert.Equal("9.99 RON", result);
        }

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(9.99, 0.01, 10)]
        public void When_TwoPricesAreSummedUp_Should_HaveCorrectAmount(double first, double second, double expected)
        {
            // Arrange
            var firstPrice = new Price(first);
            var secondPrice = new Price(second);
            var expectedPrice = new Price(expected);

            // Act
            var result = firstPrice + secondPrice;

            // Assert
            Assert.Equal(expectedPrice.ToString(), result.ToString());
        }
    }
}