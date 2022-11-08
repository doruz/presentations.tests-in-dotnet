namespace Shopping.Core;

public record Price(decimal Amount, string Currency = "EUR")
{
    public static Price operator *(Price price, int c) => new(price.Amount * c);

    public static Price operator +(Price first, Price second) => new (first.Amount + second.Amount);

    public override string ToString() => $"{Amount:F} {Currency}";
}