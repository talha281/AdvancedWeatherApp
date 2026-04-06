namespace AdvancedWeatherForecast.Web.Models.ViewModels;

public sealed class WeatherConditionViewModel
{
    public int Code { get; set; }

    public string Description { get; set; } = string.Empty;

    public string Icon { get; set; } = string.Empty;

    public bool IsDay { get; set; }
}
