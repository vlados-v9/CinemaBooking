namespace CinemaBooking.Domain;

/// <summary>
/// Temp implementation of connection string to database.
/// </summary>
public static class ConnectionStringToDb
{
    public static string ConnectionString { get; } = GetConnectionString();

    private static string GetConnectionString()
    {
        var directory = Directory.GetCurrentDirectory();

        var databasePath = Path.GetFullPath(Path.Combine(directory, "..", "CinemaBooking.DbLayer", "CinemaDb.db"));

        // Replace with your actual connection string

        return $"Data Source={databasePath}";
    }
}