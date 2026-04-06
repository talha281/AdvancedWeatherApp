namespace AdvancedWeatherForecast.Web.Models.ViewModels;

public sealed class WeatherLocationViewModel
{
    public string Name { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    public string? Admin1 { get; set; }

    public string? Timezone { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public double? ElevationMeters { get; set; }

    public int? Population { get; set; }

    public bool IsPrimary { get; set; }
}
