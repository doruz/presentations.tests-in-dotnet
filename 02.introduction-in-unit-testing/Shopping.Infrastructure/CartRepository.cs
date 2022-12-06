using Shopping.Core.Domain;
using Shopping.Core.Repositories;

namespace Shopping.Infrastructure;

public class CartRepository : ICartRepository
{
    private readonly Dictionary<string, Cart> _carts = new();

    public Cart? Find(string customerId)
    {
        return _carts.TryGetValue(customerId.ToLowerInvariant(), out Cart? cart) 
            ? cart 
            : null;
    }

    public void Save(Cart cart)
    {
        _carts[cart.CustomerId.ToLowerInvariant()] = cart;
    }
}