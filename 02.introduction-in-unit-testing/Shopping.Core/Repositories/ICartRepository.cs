using Shopping.Core.Domain;

namespace Shopping.Core.Repositories;

public interface ICartRepository
{
    Cart? Find(string customerId);

    void Save(Cart cart);
}