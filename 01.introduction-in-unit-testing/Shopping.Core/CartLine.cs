namespace Shopping.Core;

public record CartLine(CartLineProduct Product)
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public int Quantity { get; set; } = 1;

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