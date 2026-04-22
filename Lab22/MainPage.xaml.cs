using Lab22.Services;
using System.Collections.ObjectModel;
using Lab22.Models;

namespace Lab22;

public partial class MainPage : ContentPage
{
    private readonly WeatherService _service = new();
    public ObservableCollection<WeatherRecord> WeatherHistory { get; set; } = new();

    public MainPage()
    {
        InitializeComponent();
        HistoryList.ItemsSource = WeatherHistory;
        LoadData();
    }

    private async void LoadData()
    {
        var data = await _service.GetAllHistoryAsync();
        foreach (var item in data) WeatherHistory.Add(item);
    }

    private async void OnFetchBtnClicked(object sender, EventArgs e)
    {
        if (double.TryParse(LatEntry.Text, out double lat) && double.TryParse(LonEntry.Text, out double lon))
        {
            var result = await _service.GetWeatherDataAsync(lat, lon);
            if (result != null)
            {
                // Dodaj tylko jeśli nie ma go już na liście UI
                if (!WeatherHistory.Any(h => h.Time == result.Time))
                    WeatherHistory.Insert(0, result);
            }
        }
    }
}