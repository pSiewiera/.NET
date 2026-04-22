using Microsoft.EntityFrameworkCore;
using Location = Lab22.Models.Location;

namespace Lab22.Data;

public class WeatherContext : DbContext
{
    public DbSet<Lab22.Models.WeatherRecord> WeatherRecords { get; set; }
    public DbSet<Location> Locations { get; set; }

    public WeatherContext()
    {

        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "weather_new.db");
        options.UseSqlite($"Data Source={dbPath}");
    }
}