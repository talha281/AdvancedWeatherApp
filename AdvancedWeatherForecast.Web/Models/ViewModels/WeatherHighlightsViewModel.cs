namespace AdvancedWeatherForecast.Web.Models.ViewModels;

public sealed class WeatherHighlightsViewModel
{
    public int? HumidityPercent { get; set; }

    public double? WindSpeedKph { get; set; }

    public int? WindDirectionDegrees { get; set; }

    public string WindDirectionLabel { get; set; } = string.Empty;

    public double? UvIndex { get; set; }

    public double? PressureHpa { get; set; }

    public double? VisibilityKm { get; set; }

    public DateTime? Sunrise { get; set; }

    public DateTime? Sunset { get; set; }

    public int? CloudCoverPercent { get; set; }
}
