namespace Shopping.Core
{
    public record Cart
    {
        public List<CartLine> CartLines { get; init; } = new();

        public Price TotalPrice => CartLines
            .Select(line => line.TotalPrice)
            .Aggregate((p1, p2) => p1 + p2);

        public void AddNewLine(CartLineProduct product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            CartLines.Add(new CartLine(product));
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

    public record CartLineProduct(string Id, string Name, Price Price);

    public record Price(decimal Amount, string Currency = "EUR")
    {
        public static Price operator *(Price price, int c) => new(price.Amount * c);

        public static Price operator +(Price first, Price second) => new (first.Amount + second.Amount);

        public override string ToString() => $"{Amount:F} {Currency}";
    }
}