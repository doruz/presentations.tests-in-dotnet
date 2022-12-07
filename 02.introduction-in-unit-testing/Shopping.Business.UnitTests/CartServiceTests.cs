using System;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Shopping.Business.Carts;
using Shopping.Business.Products;
using Shopping.Core;
using Shopping.Core.Domain;
using Shopping.Core.Repositories;
using Xunit;

namespace Shopping.Business.UnitTests
{
    public class CartServiceTests
    {
        private const string CustomerId = "123";
        private const string ProductId = "1";

        private readonly Cart _cart;
        private readonly CartLineProduct _product;

        private readonly ICartRepository _repository;
        private readonly ProductsService _productsService;


        private readonly CartService _systemUnderTest;

        public CartServiceTests()
        {
            _cart = Cart.Empty(CustomerId);
            _product = new(ProductId, "tv", new Price(55));

            _repository = Substitute.For<ICartRepository>();
            _productsService = Substitute.For<ProductsService>();
            
            var cartSettings = new CartSettings { ExpiresAfter = TimeSpan.FromMinutes(1) };
            _systemUnderTest = new CartService(_repository, _productsService, cartSettings);

            SystemDateTime.SetDate(new DateTime(2022, 12, 7, 16, 15, 25));
        }

        [Fact]
        public void When_FindingCartWhichDoesNotExists_Should_ReturnEmpty()
        {
            // Arrange
            _repository.Find(CustomerId).ReturnsNull();

            // Act
            var result = _systemUnderTest.FindCustomerCart(CustomerId);

            // Assert
            result.Should().NotBeNull();
            result.Lines.Should().BeEmpty();
        }

        [Fact]
        public void When_FindingCartWhichIsNotExpired_Should_ReturnAllDetails()
        {
            // Arrange
            _repository.Find(CustomerId).Returns(_cart);
            _productsService.FindProduct(_product.Id).Returns(_product);

            _systemUnderTest.AddNewLine(CustomerId, new NewCartLineModel(ProductId));

            // Act
            SystemDateTime.SetDate(SystemDateTime.UtcNow.AddSeconds(59));
            var result = _systemUnderTest.FindCustomerCart(CustomerId);

            // Assert
            result.Lines.Should().HaveCount(1);
            result.TotalPrice.Should().Be("55.00 RON");
        }

        [Fact]
        public void When_FindingCartWhichIsExpired_Should_ReturnEmpty()
        {
            // Arrange
            _repository.Find(CustomerId).Returns(_cart);
            _productsService.FindProduct(_product.Id).Returns(_product);

            _systemUnderTest.AddNewLine(CustomerId, new NewCartLineModel(ProductId));
            
            // Act
            SystemDateTime.SetDate(SystemDateTime.UtcNow.AddSeconds(61));
            var result = _systemUnderTest.FindCustomerCart(CustomerId);

            // Assert
            result.Should().NotBeNull();
            result.Lines.Should().BeEmpty();
        }

        [Fact]
        public void When_AddingNewLineWithInvalidProduct_Should_NotSaveCart()
        {
            // Act
            _systemUnderTest.AddNewLine(CustomerId, new NewCartLineModel(ProductId));

            // Assert
            _repository
                .DidNotReceive()
                .Save(Arg.Any<Cart>());
        }

        [Fact]
        public void When_AddingNewLineWithValidProduct_Should_SaveCartOnlyOnce()
        {
            // Arrange
            _repository.Find(CustomerId).ReturnsNull();
            _productsService.FindProduct(_product.Id).Returns(_product);

            // Act
            _systemUnderTest.AddNewLine(CustomerId, new NewCartLineModel(ProductId));

            // Assert
            _repository
                .Received(1)
                .Save(Arg.Any<Cart>());
        }

        [Fact]
        public void When_AddingNewLineOnExistingCart_Should_ExpirationDateTimeShouldBeExtended()
        {
            // Arrange
            _repository.Find(CustomerId).Returns(_cart);
            _productsService.FindProduct(_product.Id).Returns(_product);

            // Act
            _systemUnderTest.AddNewLine(CustomerId, new NewCartLineModel(ProductId));

            // Assert
            _cart.ExpiresAt
                .Should()
                .Be(SystemDateTime.UtcNow.AddMinutes(1));
        }

        [Fact]
        public void When_AddingNewLineOnNewCart_Should_BeSavedWithCorrectExpirationDateTime()
        {
            // Arrange
            _repository.Find(CustomerId).ReturnsNull();
            _productsService.FindProduct(_product.Id).Returns(_product);

            // Act
            _systemUnderTest.AddNewLine(CustomerId, new NewCartLineModel(ProductId));

            // Assert
            _repository
                .Received(1)
                .Save(Arg.Is<Cart>(c => c.ExpiresAt == SystemDateTime.UtcNow.AddMinutes(1)));
        }
    }
}