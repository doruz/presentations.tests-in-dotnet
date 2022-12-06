namespace Shopping.Business.Carts;

public record CartSettings
{
    public TimeSpan ExpiresAfter { get; init; }
}