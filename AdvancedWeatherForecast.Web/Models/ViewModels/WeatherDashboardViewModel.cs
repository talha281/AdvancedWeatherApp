namespace AdvancedWeatherForecast.Web.Models.ViewModels;

public sealed class WeatherDashboardViewModel
{
    public string SearchTerm { get; set; } = string.Empty;

    public WeatherLocationViewModel? SelectedLocation { get; set; }

    public IReadOnlyList<WeatherLocationViewModel> SearchResults { get; set; } = [];

    public WeatherCurrentViewModel? Current { get; set; }

    public IReadOnlyList<WeatherHourlyForecastViewModel> HourlyForecast { get; set; } = [];

    public IReadOnlyList<WeatherDailyForecastViewModel> DailyForecast { get; set; } = [];

    public WeatherHighlightsViewModel? Highlights { get; set; }

    public IReadOnlyList<string> Alerts { get; set; } = [];

    public DateTimeOffset? LastUpdated { get; set; }

    public string? ErrorMessage { get; set; }

    public bool HasForecast => Current is not null && DailyForecast.Count > 0;
}
