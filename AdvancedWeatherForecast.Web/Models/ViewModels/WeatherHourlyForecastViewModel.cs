namespace AdvancedWeatherForecast.Web.Models.ViewModels;

public sealed class WeatherHourlyForecastViewModel
{
    public DateTime Time { get; set; }

    public double TemperatureCelsius { get; set; }

    public double ApparentTemperatureCelsius { get; set; }

    public int? RelativeHumidityPercent { get; set; }

    public int? PrecipitationProbabilityPercent { get; set; }

    public double? PrecipitationMm { get; set; }

    public double? RainMm { get; set; }

    public double? ShowersMm { get; set; }

    public double? SnowfallCm { get; set; }

    public int WeatherCode { get; set; }

    public double? CloudCoverPercent { get; set; }

    public double? PressureHpa { get; set; }

    public double? VisibilityKm { get; set; }

    public double? WindSpeedKph { get; set; }

    public int? WindDirectionDegrees { get; set; }

    public double? WindGustsKph { get; set; }

    public double? UvIndex { get; set; }

    public bool IsDay { get; set; }

    public WeatherConditionViewModel Condition { get; set; } = new();
}
