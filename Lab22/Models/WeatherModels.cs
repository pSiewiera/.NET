using System.ComponentModel.DataAnnotations;

namespace Lab22.Models;

public class Location
{
    [Key]
    public int Id { get; set; }
    public required string CityName { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    // Relacja: Jedna lokalizacja -> wiele rekordów pogodowych
    public List<WeatherRecord> WeatherRecords { get; set; } = new();
}

public class WeatherRecord
{
    [Key]
    public int Id { get; set; }
    public double Temperature { get; set; }
    public double WindSpeed { get; set; }
    public int WindDirection { get; set; }
    public string Time { get; set; } = string.Empty;

    // Klucz obcy do relacji
    public int LocationId { get; set; }
    public Location? Location { get; set; }
}

// Klasy pomocnicze do deserializacji JSON (z Twojego kodu)
public class WeatherResponse
{
    public CurrentWeather current_weather { get; set; }
}

public class CurrentWeather
{
    public double temperature { get; set; }
    public double windspeed { get; set; }
    public int winddirection { get; set; }
    public string time { get; set; }
}