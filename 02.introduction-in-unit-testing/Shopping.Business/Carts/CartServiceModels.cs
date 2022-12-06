namespace Shopping.Business.Carts;

public record NewCartLineModel(string ProductId);

public record CartModel
{
    public string TotalPrice { get; set; } = default!;
    public IEnumerable<CartLineModel> Lines { get; set; } = default!;
}

public record CartLineModel
{
    public Guid Id { get; init; }
    public string Description { get; init; } = default!;
    public string TotalPrice { get; set; } = default!;
}