namespace Shopping.Core;

public record Price(decimal Amount)
{
    public string Currency => "EUR";

    public static Price operator *(Price price, int multiplier) => new(price.Amount * multiplier);

    public static Price operator +(Price first, Price second) => new (first.Amount + second.Amount);

    public override string ToString() => $"{Amount:F} {Currency}";
}