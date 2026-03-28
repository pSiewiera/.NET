using System.Text.Json;

string url = $"https://api.open-meteo.com/v1/forecast?latitude=51.1079&longitude=17.0385&current_weather=true&relative_humidity=true";
using HttpClient client = new();

string json = await client.GetStringAsync(url);

WeatherData data = JsonSerializer.Deserialize<WeatherData>(json);

var w = data.current_weather;
Console.WriteLine($"Temperature: {w.temperature}C");
Console.WriteLine($"Wind Speed: {w.windspeed} km/h");
Console.WriteLine($"Wind Direction: {w.winddirection}");
Console.WriteLine($"Time: {w.time}");

public class CurrentWeather
{
    public double temperature { get; set; }
    public double windspeed { get; set; }
    public int winddirection { get; set; }
    public string time { get; set; }
}
public class WeatherData
{
    public CurrentWeather current_weather { get; set; }
}