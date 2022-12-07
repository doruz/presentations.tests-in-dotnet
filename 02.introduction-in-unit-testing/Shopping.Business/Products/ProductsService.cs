using Shopping.Core.Domain;

namespace Shopping.Business.Products;

public abstract class ProductsService
{
    public abstract IEnumerable<CartLineProduct> GetAll();

    public abstract CartLineProduct? FindProduct(string id);
}