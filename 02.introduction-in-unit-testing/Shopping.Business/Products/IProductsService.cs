using Shopping.Core.Domain;

namespace Shopping.Business.Products;

public interface IProductsService
{
    IEnumerable<CartLineProduct> GetAll();

    CartLineProduct? FindProduct(string id);
}