namespace CinemaBooking.Domain;

/// <summary>
/// Temp implementation of connection string to database.
/// </summary>
public static class ConnectionStringToDb
{
    public static string ConnectionString { get; } = GetConnectionString();

    private static string GetConnectionString()
    {
        var solutionDirectory = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;

        var databasePath = Path.Combine(solutionDirectory, "CinemaDb.db");

        // Replace with your actual connection string

        return $"Data Source={databasePath}";
    }
}