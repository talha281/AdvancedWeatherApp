using System.Text.Json.Serialization;

namespace AdvancedWeatherForecast.Web.Models.OpenMeteo;

public sealed class OpenMeteoForecastResponse
{
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonPropertyName("elevation")]
    public double? Elevation { get; set; }

    [JsonPropertyName("generationtime_ms")]
    public double GenerationTimeMs { get; set; }

    [JsonPropertyName("utc_offset_seconds")]
    public int UtcOffsetSeconds { get; set; }

    [JsonPropertyName("timezone")]
    public string? Timezone { get; set; }

    [JsonPropertyName("timezone_abbreviation")]
    public string? TimezoneAbbreviation { get; set; }

    [JsonPropertyName("current")]
    public OpenMeteoCurrentWeatherDto? Current { get; set; }

    [JsonPropertyName("hourly")]
    public OpenMeteoHourlyWeatherDto? Hourly { get; set; }

    [JsonPropertyName("daily")]
    public OpenMeteoDailyWeatherDto? Daily { get; set; }

    [JsonPropertyName("error")]
    public bool? Error { get; set; }

    [JsonPropertyName("reason")]
    public string? ErrorReason { get; set; }
}

public sealed class OpenMeteoCurrentWeatherDto
{
    [JsonPropertyName("time")]
    public string? Time { get; set; }

    [JsonPropertyName("interval")]
    public int Interval { get; set; }

    [JsonPropertyName("temperature_2m")]
    public double Temperature2M { get; set; }

    [JsonPropertyName("relative_humidity_2m")]
    public int RelativeHumidity2M { get; set; }

    [JsonPropertyName("apparent_temperature")]
    public double ApparentTemperature { get; set; }

    [JsonPropertyName("is_day")]
    public int IsDay { get; set; }

    [JsonPropertyName("precipitation")]
    public double Precipitation { get; set; }

    [JsonPropertyName("rain")]
    public double Rain { get; set; }

    [JsonPropertyName("showers")]
    public double Showers { get; set; }

    [JsonPropertyName("snowfall")]
    public double Snowfall { get; set; }

    [JsonPropertyName("weather_code")]
    public int WeatherCode { get; set; }

    [JsonPropertyName("cloud_cover")]
    public int CloudCover { get; set; }

    [JsonPropertyName("pressure_msl")]
    public double PressureMsl { get; set; }

    [JsonPropertyName("surface_pressure")]
    public double SurfacePressure { get; set; }

    [JsonPropertyName("visibility")]
    public double? Visibility { get; set; }

    [JsonPropertyName("wind_speed_10m")]
    public double WindSpeed10M { get; set; }

    [JsonPropertyName("wind_direction_10m")]
    public int WindDirection10M { get; set; }

    [JsonPropertyName("wind_gusts_10m")]
    public double WindGusts10M { get; set; }

    [JsonPropertyName("uv_index")]
    public double? UvIndex { get; set; }
}

public sealed class OpenMeteoHourlyWeatherDto
{
    [JsonPropertyName("time")]
    public List<string> Time { get; set; } = [];

    [JsonPropertyName("temperature_2m")]
    public List<double?> Temperature2M { get; set; } = [];

    [JsonPropertyName("apparent_temperature")]
    public List<double?> ApparentTemperature { get; set; } = [];

    [JsonPropertyName("relative_humidity_2m")]
    public List<int?> RelativeHumidity2M { get; set; } = [];

    [JsonPropertyName("precipitation_probability")]
    public List<int?> PrecipitationProbability { get; set; } = [];

    [JsonPropertyName("precipitation")]
    public List<double?> Precipitation { get; set; } = [];

    [JsonPropertyName("rain")]
    public List<double?> Rain { get; set; } = [];

    [JsonPropertyName("showers")]
    public List<double?> Showers { get; set; } = [];

    [JsonPropertyName("snowfall")]
    public List<double?> Snowfall { get; set; } = [];

    [JsonPropertyName("weather_code")]
    public List<int?> WeatherCode { get; set; } = [];

    [JsonPropertyName("cloud_cover")]
    public List<double?> CloudCover { get; set; } = [];

    [JsonPropertyName("pressure_msl")]
    public List<double?> PressureMsl { get; set; } = [];

    [JsonPropertyName("visibility")]
    public List<double?> Visibility { get; set; } = [];

    [JsonPropertyName("wind_speed_10m")]
    public List<double?> WindSpeed10M { get; set; } = [];

    [JsonPropertyName("wind_direction_10m")]
    public List<int?> WindDirection10M { get; set; } = [];

    [JsonPropertyName("wind_gusts_10m")]
    public List<double?> WindGusts10M { get; set; } = [];

    [JsonPropertyName("uv_index")]
    public List<double?> UvIndex { get; set; } = [];

    [JsonPropertyName("is_day")]
    public List<int?> IsDay { get; set; } = [];
}

public sealed class OpenMeteoDailyWeatherDto
{
    [JsonPropertyName("time")]
    public List<string> Time { get; set; } = [];

    [JsonPropertyName("weather_code")]
    public List<int?> WeatherCode { get; set; } = [];

    [JsonPropertyName("temperature_2m_max")]
    public List<double?> Temperature2MMax { get; set; } = [];

    [JsonPropertyName("temperature_2m_min")]
    public List<double?> Temperature2MMin { get; set; } = [];

    [JsonPropertyName("apparent_temperature_max")]
    public List<double?> ApparentTemperatureMax { get; set; } = [];

    [JsonPropertyName("apparent_temperature_min")]
    public List<double?> ApparentTemperatureMin { get; set; } = [];

    [JsonPropertyName("sunrise")]
    public List<string> Sunrise { get; set; } = [];

    [JsonPropertyName("sunset")]
    public List<string> Sunset { get; set; } = [];

    [JsonPropertyName("daylight_duration")]
    public List<double?> DaylightDuration { get; set; } = [];

    [JsonPropertyName("sunshine_duration")]
    public List<double?> SunshineDuration { get; set; } = [];

    [JsonPropertyName("precipitation_sum")]
    public List<double?> PrecipitationSum { get; set; } = [];

    [JsonPropertyName("rain_sum")]
    public List<double?> RainSum { get; set; } = [];

    [JsonPropertyName("showers_sum")]
    public List<double?> ShowersSum { get; set; } = [];

    [JsonPropertyName("snowfall_sum")]
    public List<double?> SnowfallSum { get; set; } = [];

    [JsonPropertyName("precipitation_probability_max")]
    public List<int?> PrecipitationProbabilityMax { get; set; } = [];

    [JsonPropertyName("wind_speed_10m_max")]
    public List<double?> WindSpeed10MMax { get; set; } = [];

    [JsonPropertyName("wind_gusts_10m_max")]
    public List<double?> WindGusts10MMax { get; set; } = [];

    [JsonPropertyName("wind_direction_10m_dominant")]
    public List<int?> WindDirection10MDominant { get; set; } = [];

    [JsonPropertyName("uv_index_max")]
    public List<double?> UvIndexMax { get; set; } = [];
}
