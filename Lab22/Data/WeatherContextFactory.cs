using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Lab22.Data;

public class WeatherContextFactory : IDesignTimeDbContextFactory<WeatherContext>
{
    public WeatherContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<WeatherContext>();
        // Używamy prostej ścieżki pliku dla Windows na czas migracji
        optionsBuilder.UseSqlite("Data Source=design_time_weather.db");

        return new WeatherContext(optionsBuilder.Options);
    }
}