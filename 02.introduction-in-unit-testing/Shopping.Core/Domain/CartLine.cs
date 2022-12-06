namespace Shopping.Core.Domain;

public record CartLine(CartLineProduct Product)
{
    public Guid Id { get; } = Guid.NewGuid();

    public int Quantity { get; private set; } = 1;

    public Price TotalPrice => Product.Price * Quantity;

    public void ChangeQuantity(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "quantity must be positive");
        }

        Quantity = quantity;
    }
}