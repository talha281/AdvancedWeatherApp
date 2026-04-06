namespace AdvancedWeatherForecast.Web.Models.ViewModels;

public sealed class WeatherDailyForecastViewModel
{
    public DateOnly Date { get; set; }

    public DateTime Sunrise { get; set; }

    public DateTime Sunset { get; set; }

    public double MaxTemperatureCelsius { get; set; }

    public double MinTemperatureCelsius { get; set; }

    public double ApparentMaxTemperatureCelsius { get; set; }

    public double ApparentMinTemperatureCelsius { get; set; }

    public double? PrecipitationMm { get; set; }

    public double? RainMm { get; set; }

    public double? ShowersMm { get; set; }

    public double? SnowfallCm { get; set; }

    public int? PrecipitationProbabilityMaxPercent { get; set; }

    public double? WindSpeedMaxKph { get; set; }

    public double? WindGustsMaxKph { get; set; }

    public int? DominantWindDirectionDegrees { get; set; }

    public double? DaylightDurationSeconds { get; set; }

    public double? SunshineDurationSeconds { get; set; }

    public double? UvIndexMax { get; set; }

    public WeatherConditionViewModel Condition { get; set; } = new();
}
