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
}