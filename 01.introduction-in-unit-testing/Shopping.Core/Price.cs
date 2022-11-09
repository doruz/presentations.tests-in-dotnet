namespace Shopping.Core;

public record Price
{
    public double Amount { get; }

    public Price(double amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }

        Amount = amount;
    }

    public string Currency => "RON";

    public static Price operator *(Price price, int multiplier) 
        => new(price.Amount * multiplier);

    public static Price operator +(Price first, Price second) 
        => new (first.Amount + second.Amount);

    public override string ToString() 
        => $"{Amount:F} {Currency}";
}