using Microsoft.EntityFrameworkCore;

namespace AdminWatchServer;

public class AdminWatchContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public string DbPath { get; }

    public AdminWatchContext()
    {
        var path = Path.Join(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "AdminWatch");
        DbPath = Path.Join(path, "database.db");

        Console.WriteLine($"Database path: {DbPath}");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}