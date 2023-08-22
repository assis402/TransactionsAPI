namespace Transactions.Shared.Helpers;

public static class Utils
{
    public static DateTime BrazilDateTime()
    {
        var timeUtc = DateTime.UtcNow;
        var timeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
        var test = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, timeZone);
        return test;
    }

    public static bool ValidateEnum<TEnum>(string value) where TEnum : Enum
        => Enum.GetNames(typeof(TEnum)).Any(x => x.ToLower() == value.ToLower());

    public static string FirstCharToLowerCase(this string @string)
        => char.ToLowerInvariant(@string[0]) + @string[1..];
}