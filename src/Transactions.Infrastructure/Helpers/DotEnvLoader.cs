namespace Transactions.Infrastructure.Helpers;

public static class DotEnvLoader
{
    public static void Load()
    {
        var root = Directory.GetCurrentDirectory();
        var filePath = Path.GetFullPath(Path.Combine(root, @"..\.env"));

        if (!File.Exists(filePath))
            return;

        foreach (var line in File.ReadAllLines(filePath))
        {
            var parts = line.Split('=', 2, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)
                continue;

            Environment.SetEnvironmentVariable(parts[0], parts[1]);
        }
    }
}