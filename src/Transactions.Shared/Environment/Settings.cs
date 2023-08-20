namespace Transactions.Shared.Environment;

public static class Settings
{
    public static readonly string ApplicationBaseUrl = System.Environment.GetEnvironmentVariable("APPLICATION_BASE_URL");
    public static readonly string ConnectionString = System.Environment.GetEnvironmentVariable("CONNECTION_STRING");
    public static readonly string Database = System.Environment.GetEnvironmentVariable("DATABASE");
}