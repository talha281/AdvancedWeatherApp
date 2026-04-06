using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json;
using AdvancedWeatherForecast.Web.Models.OpenMeteo;
using AdvancedWeatherForecast.Web.Options;
using Microsoft.Extensions.Options;

namespace AdvancedWeatherForecast.Web.Services;

public sealed class OpenMeteoClient : IOpenMeteoClient
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly OpenMeteoOptions _options;

    public OpenMeteoClient(IHttpClientFactory httpClientFactory, IOptions<OpenMeteoOptions> options)
    {
        _httpClientFactory = httpClientFactory;
        _options = options.Value;
    }

    public async Task<IReadOnlyList<OpenMeteoLocationDto>> SearchLocationsAsync(string city, CancellationToken cancellationToken)
    {
        var client = _httpClientFactory.CreateClient(OpenMeteoClientNames.Geocoding);
        var url = $"v1/search?name={Uri.EscapeDataString(city)}&count=5&language=en&format=json";
        var response = await client.GetAsync(url, cancellationToken);
        var payload = await response.Content.ReadFromJsonAsync<OpenMeteoGeocodingResponse>(JsonOptions, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var error = payload?.Reason ?? $"Open-Meteo geocoding request failed with status code {(int)response.StatusCode}.";
            throw new HttpRequestException(error);
        }

        return payload?.Results ?? [];
    }

    public async Task<OpenMeteoForecastResponse?> GetForecastAsync(OpenMeteoLocationDto location, CancellationToken cancellationToken)
    {
        var client = _httpClientFactory.CreateClient(OpenMeteoClientNames.Forecast);
        var query = BuildForecastQuery(location);
        var response = await client.GetAsync(query, cancellationToken);
        var payload = await response.Content.ReadFromJsonAsync<OpenMeteoForecastResponse>(JsonOptions, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var error = payload?.ErrorReason ?? $"Open-Meteo forecast request failed with status code {(int)response.StatusCode}.";
            throw new HttpRequestException(error);
        }

        return payload;
    }

    private string BuildForecastQuery(OpenMeteoLocationDto location)
    {
        var current = string.Join(",",
            "temperature_2m",
            "relative_humidity_2m",
            "apparent_temperature",
            "is_day",
            "precipitation",
            "rain",
            "showers",
            "snowfall",
            "weather_code",
            "cloud_cover",
            "pressure_msl",
            "surface_pressure",
            "visibility",
            "wind_speed_10m",
            "wind_direction_10m",
            "wind_gusts_10m",
            "uv_index");

        var hourly = string.Join(",",
            "temperature_2m",
            "apparent_temperature",
            "relative_humidity_2m",
            "precipitation_probability",
            "precipitation",
            "rain",
            "showers",
            "snowfall",
            "weather_code",
            "cloud_cover",
            "pressure_msl",
            "visibility",
            "wind_speed_10m",
            "wind_direction_10m",
            "wind_gusts_10m",
            "uv_index",
            "is_day");

        var daily = string.Join(",",
            "weather_code",
            "temperature_2m_max",
            "temperature_2m_min",
            "apparent_temperature_max",
            "apparent_temperature_min",
            "sunrise",
            "sunset",
            "daylight_duration",
            "sunshine_duration",
            "precipitation_sum",
            "rain_sum",
            "showers_sum",
            "snowfall_sum",
            "precipitation_probability_max",
            "wind_speed_10m_max",
            "wind_gusts_10m_max",
            "wind_direction_10m_dominant",
            "uv_index_max");

        return $"v1/forecast?latitude={location.Latitude.ToString(CultureInfo.InvariantCulture)}" +
               $"&longitude={location.Longitude.ToString(CultureInfo.InvariantCulture)}" +
               $"&current={Uri.EscapeDataString(current)}" +
               $"&hourly={Uri.EscapeDataString(hourly)}" +
               $"&daily={Uri.EscapeDataString(daily)}" +
               $"&timezone={Uri.EscapeDataString(string.IsNullOrWhiteSpace(_options.Timezone) ? "auto" : _options.Timezone)}" +
               $"&forecast_days={Math.Clamp(_options.ForecastDays, 1, 16)}" +
               $"&temperature_unit={Uri.EscapeDataString(_options.TemperatureUnit)}" +
               $"&wind_speed_unit={Uri.EscapeDataString(_options.WindSpeedUnit)}" +
               $"&precipitation_unit={Uri.EscapeDataString(_options.PrecipitationUnit)}" +
               $"&format=json";
    }
}
