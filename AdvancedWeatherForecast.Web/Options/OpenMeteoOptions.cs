namespace AdvancedWeatherForecast.Web.Options;

public sealed class OpenMeteoOptions
{
    public const string SectionName = "OpenMeteo";

    public string GeocodingBaseUrl { get; set; } = "https://geocoding-api.open-meteo.com/";

    public string ForecastBaseUrl { get; set; } = "https://api.open-meteo.com/";

    public int WeatherCacheMinutes { get; set; } = 5;

    public int GeocodingCacheMinutes { get; set; } = 10;

    public int ForecastDays { get; set; } = 7;

    public string DefaultCity { get; set; } = "New York";

    public string Timezone { get; set; } = "auto";

    public string TemperatureUnit { get; set; } = "celsius";

    public string WindSpeedUnit { get; set; } = "kmh";

    public string PrecipitationUnit { get; set; } = "mm";
}
