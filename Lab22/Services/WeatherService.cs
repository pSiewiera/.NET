using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Lab22.Data;
using Lab22.Models;

namespace Lab22.Services;
using Location = Lab22.Models.Location;
public class WeatherService
{
    private readonly WeatherContext _db = new();
    private readonly HttpClient _client = new();

    public async Task<WeatherRecord?> GetWeatherDataAsync(double lat, double lon)
    {
        // 1. Sprawdź czy mamy tę lokalizację w bazie
        var location = await _db.Locations
            .FirstOrDefaultAsync(l => l.Latitude == lat && l.Longitude == lon);

        if (location == null)
        {
            location = new Location { CityName = "Punkt", Latitude = lat, Longitude = lon };
            _db.Locations.Add(location);
            await _db.SaveChangesAsync();
        }

        // 2. Pobierz "podgląd" z API, żeby sprawdzić czas pomiaru
        string url = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current_weather=true";
        string json = await _client.GetStringAsync(url);
        var response = JsonSerializer.Deserialize<WeatherResponse>(json);
        var apiWeather = response.current_weather;

        // 3. Sprawdź czy rekord z tym czasem już jest w bazie (unikamy duplikatów)
        var existing = await _db.WeatherRecords
            .FirstOrDefaultAsync(r => r.LocationId == location.Id && r.Time == apiWeather.time);

        if (existing != null) return existing;

        // 4. Jeśli nie ma - dodaj do bazy
        var newRecord = new WeatherRecord
        {
            Temperature = apiWeather.temperature,
            WindSpeed = apiWeather.windspeed,
            WindDirection = apiWeather.winddirection,
            Time = apiWeather.time,
            LocationId = location.Id
        };

        _db.WeatherRecords.Add(newRecord);
        await _db.SaveChangesAsync();
        return newRecord;
    }

    public async Task<List<WeatherRecord>> GetAllHistoryAsync() 
        => await _db.WeatherRecords.OrderByDescending(r => r.Time).ToListAsync();
}