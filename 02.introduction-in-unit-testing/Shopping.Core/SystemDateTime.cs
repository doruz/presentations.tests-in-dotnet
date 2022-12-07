namespace Shopping.Core;

public static class SystemDateTime
{
    private static Func<DateTime> _utcNow = () => DateTime.Now;

    public static DateTime UtcNow => _utcNow();

    public static void SetDate(DateTime expectedDateTime) => _utcNow = () => expectedDateTime;
}