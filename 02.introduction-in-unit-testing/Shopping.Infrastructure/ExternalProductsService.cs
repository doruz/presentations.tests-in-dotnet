using Shopping.Business.Products;
using Shopping.Core.Domain;

namespace Shopping.Infrastructure;

public sealed class ExternalProductsService : IProductsService
{
    private static readonly List<CartLineProduct> Products = new()
    {
        new CartLineProduct("101", "Mouse", new Price(57.50)),
        new CartLineProduct("102", "Keyboard", new Price(89.89)),
        new CartLineProduct("103", "Headset", new Price(320)),
        new CartLineProduct("104", "Monitor", new Price(999.99)),
        new CartLineProduct("105", "Laptop", new Price(4750)),
    };

    public IEnumerable<CartLineProduct> GetAll() 
        => Products;

    public CartLineProduct? FindProduct(string id) 
        => Products.FirstOrDefault(p => p.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase));
}