namespace Shopping.Core.Domain;

public record Cart
{
    public string CustomerId { get; init; } = default!;

    public DateTime ExpiresAt { get; set; } = DateTime.MaxValue;

    public List<CartLine> CartLines { get; init; } = new();

    public Price TotalPrice => CartLines
        .Select(line => line.TotalPrice)
        .Aggregate(new Price(0), (p1, p2) => p1 + p2);

    public bool IsExpired() => ExpiresAt <= DateTime.UtcNow;

    public static Cart Empty(string customerId) => new() { CustomerId = customerId.ToLowerInvariant() };

    public void AddNewLine(CartLineProduct product)
    {
        if (product is null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        CartLines.Add(new CartLine(product));
    }

    public void ExpiresAfter(TimeSpan time)
    {
        if (time <= TimeSpan.Zero)
        {
            throw new ArgumentException("expiration time should be positive", nameof(time));
        }
        
        ExpiresAt = DateTime.UtcNow.Add(time);
    }

    public void UpdateExistingLine(Guid lineId, int quantity)
    {
        if (quantity == 0)
        {
            RemoveLine(lineId);
        }
        else
        {
            FindLine(lineId).ChangeQuantity(quantity);
        }
    }

    public void RemoveLine(Guid lineId) => CartLines.RemoveAll(line => line.Id.Equals(lineId));

    private CartLine FindLine(Guid id) => CartLines.First(line => line.Id.Equals(id));
}