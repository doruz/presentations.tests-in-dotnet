using Shopping.Business.Products;
using Shopping.Core.Domain;
using Shopping.Core.Repositories;

namespace Shopping.Business.Carts;

public sealed class CartService
{
    private readonly ICartRepository _repository;
    private readonly ProductsService _productsService;
    private readonly CartSettings _settings;

    public CartService(ICartRepository repository, ProductsService productsService, CartSettings settings)
    {
        _repository = repository;
        _productsService = productsService;
        _settings = settings;
    }

    public CartModel FindCustomerCart(string customerId)
    {
        var cart = FindCart(customerId);
        
        return ToCartModel(cart);
    }

    public void AddNewLine(string customerId, NewCartLineModel line)
    {
        var cart = FindCart(customerId);
        var product = _productsService.FindProduct(line.ProductId);

        if (product is not null)
        {
            cart.AddNewLine(product);
            cart.ExpiresAfter(_settings.ExpiresAfter);

            _repository.Save(cart);
        }
    }

    private Cart FindCart(string customerId)
    {
        var cart = _repository.Find(customerId) ?? Cart.Empty(customerId);

        // cart.ExpiresAt <= DateTime.UtcNow;
        if (cart.IsExpired())
        {
            cart = Cart.Empty(customerId);
        }

        return cart;
    }

    private CartModel ToCartModel(Cart cart)
    {
        CartLineModel ToCartLineModel(CartLine line) => new()
        {
            Id = line.Id,
            Description = $"{line.Quantity} x {line.Product.Name}",
            TotalPrice = line.TotalPrice.ToString()
        };

        return new CartModel
        {
            TotalPrice = cart.TotalPrice.ToString(),
            Lines = cart.CartLines.Select(ToCartLineModel).ToList()
        };
    }
}