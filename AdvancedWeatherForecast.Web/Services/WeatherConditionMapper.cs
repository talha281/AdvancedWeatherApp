using AdvancedWeatherForecast.Web.Models.ViewModels;

namespace AdvancedWeatherForecast.Web.Services;

internal static class WeatherConditionMapper
{
    public static WeatherConditionViewModel Map(int weatherCode, bool isDay)
    {
        var (description, icon) = weatherCode switch
        {
            0 => ("Clear sky", isDay ? "sun" : "moon-stars"),
            1 => ("Mainly clear", isDay ? "cloud-sun" : "cloud-moon"),
            2 => ("Partly cloudy", "cloud-sun"),
            3 => ("Overcast", "cloud"),
            45 or 48 => ("Fog", "cloud-fog"),
            51 => ("Light drizzle", "cloud-drizzle"),
            53 => ("Drizzle", "cloud-drizzle"),
            55 => ("Dense drizzle", "cloud-drizzle"),
            56 or 57 => ("Freezing drizzle", "snow"),
            61 => ("Light rain", "cloud-rain"),
            63 => ("Rain", "cloud-rain"),
            65 => ("Heavy rain", "cloud-rain-heavy"),
            66 or 67 => ("Freezing rain", "cloud-snow"),
            71 => ("Light snow", "snow"),
            73 => ("Snow", "snow"),
            75 => ("Heavy snow", "snow-heavy"),
            77 => ("Snow grains", "snow"),
            80 => ("Rain showers", "cloud-showers"),
            81 => ("Rain showers", "cloud-showers-heavy"),
            82 => ("Violent rain showers", "cloud-showers-heavy"),
            85 => ("Snow showers", "cloud-snow"),
            86 => ("Heavy snow showers", "cloud-snow"),
            95 => ("Thunderstorm", "cloud-lightning"),
            96 or 99 => ("Thunderstorm with hail", "cloud-lightning-rain"),
            _ => ("Weather update", "thermometer-half")
        };

        return new WeatherConditionViewModel
        {
            Code = weatherCode,
            Description = description,
            Icon = icon,
            IsDay = isDay
        };
    }
}
