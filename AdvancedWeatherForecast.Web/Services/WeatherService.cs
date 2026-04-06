using System.Globalization;
using AdvancedWeatherForecast.Web.Models.OpenMeteo;
using AdvancedWeatherForecast.Web.Models.ViewModels;
using AdvancedWeatherForecast.Web.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace AdvancedWeatherForecast.Web.Services;

public sealed class WeatherService : IWeatherService
{
    private readonly IOpenMeteoClient _client;
    private readonly IMemoryCache _cache;
    private readonly OpenMeteoOptions _options;
    private readonly ILogger<WeatherService> _logger;

    public WeatherService(
        IOpenMeteoClient client,
        IMemoryCache cache,
        IOptions<OpenMeteoOptions> options,
        ILogger<WeatherService> logger)
    {
        _client = client;
        _cache = cache;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<WeatherDashboardViewModel> GetDashboardAsync(string? city, CancellationToken cancellationToken)
    {
        var searchTerm = string.IsNullOrWhiteSpace(city) ? _options.DefaultCity : city.Trim();
        var model = new WeatherDashboardViewModel
        {
            SearchTerm = searchTerm
        };

        try
        {
            var locations = await GetCachedAsync(
                BuildGeocodingCacheKey(searchTerm),
                TimeSpan.FromMinutes(_options.GeocodingCacheMinutes),
                () => _client.SearchLocationsAsync(searchTerm, cancellationToken));

            if (!locations.Any())
            {
                model.ErrorMessage = $"We could not find a city matching '{searchTerm}'. Try a fuller name.";
                return model;
            }

            var selectedLocation = locations[0];
            model.SearchResults = locations.Select((location, index) => MapLocation(location, index == 0)).ToList();
            model.SelectedLocation = MapLocation(selectedLocation, true);

            var forecast = await GetCachedAsync(
                BuildForecastCacheKey(selectedLocation),
                TimeSpan.FromMinutes(_options.WeatherCacheMinutes),
                () => _client.GetForecastAsync(selectedLocation, cancellationToken));

            if (forecast is null)
            {
                model.ErrorMessage = $"Weather data for '{selectedLocation.DisplayName}' is temporarily unavailable.";
                return model;
            }

            PopulateForecast(model, selectedLocation, forecast);
            model.LastUpdated = DateTimeOffset.UtcNow;
            return model;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to load weather dashboard for {City}", searchTerm);
            model.ErrorMessage = "We hit a weather service issue. Please try again in a moment.";
            return model;
        }
    }

    private async Task<T> GetCachedAsync<T>(string key, TimeSpan duration, Func<Task<T>> factory)
    {
        if (_cache.TryGetValue(key, out T? cached) && cached is not null)
        {
            return cached;
        }

        var value = await factory();
        _cache.Set(key, value, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = duration,
            Priority = CacheItemPriority.High
        });

        return value;
    }

    private static string BuildGeocodingCacheKey(string city) => $"weather:geo:{NormalizeKey(city)}";

    private static string BuildForecastCacheKey(OpenMeteoLocationDto location) =>
        $"weather:forecast:{location.Latitude.ToString("F4", CultureInfo.InvariantCulture)}:{location.Longitude.ToString("F4", CultureInfo.InvariantCulture)}";

    private static string NormalizeKey(string value) => value.Trim().ToLowerInvariant();

    private static WeatherLocationViewModel MapLocation(OpenMeteoLocationDto location, bool isPrimary)
    {
        return new WeatherLocationViewModel
        {
            Name = location.Name ?? string.Empty,
            DisplayName = location.DisplayName,
            Country = location.Country ?? string.Empty,
            Admin1 = location.Admin1,
            Timezone = location.Timezone ?? "auto",
            Latitude = location.Latitude,
            Longitude = location.Longitude,
            ElevationMeters = location.Elevation,
            Population = location.Population,
            IsPrimary = isPrimary
        };
    }

    private static void PopulateForecast(
        WeatherDashboardViewModel model,
        OpenMeteoLocationDto location,
        OpenMeteoForecastResponse forecast)
    {
        var current = forecast.Current;
        var hourly = forecast.Hourly;
        var daily = forecast.Daily;

        if (current is null || hourly is null || daily is null)
        {
            model.ErrorMessage = $"Weather data for '{location.DisplayName}' is incomplete.";
            return;
        }

        model.Current = new WeatherCurrentViewModel
        {
            ObservedAt = ParseLocalDateTime(current.Time),
            TemperatureCelsius = current.Temperature2M,
            ApparentTemperatureCelsius = current.ApparentTemperature,
            RelativeHumidityPercent = current.RelativeHumidity2M,
            PressureHpa = current.PressureMsl,
            VisibilityKm = ToKilometers(current.Visibility),
            UvIndex = current.UvIndex,
            WindSpeedKph = current.WindSpeed10M,
            WindDirectionDegrees = current.WindDirection10M,
            WindGustsKph = current.WindGusts10M,
            PrecipitationMm = current.Precipitation,
            RainMm = current.Rain,
            ShowersMm = current.Showers,
            SnowfallCm = current.Snowfall,
            CloudCoverPercent = current.CloudCover,
            IsDay = current.IsDay == 1,
            Condition = WeatherConditionMapper.Map(current.WeatherCode, current.IsDay == 1)
        };

        var hourlyCount = hourly.Time.Count;
        var hourlyItems = new List<WeatherHourlyForecastViewModel>(hourlyCount);

        for (var i = 0; i < hourlyCount; i++)
        {
            var weatherCode = IntValueAt(hourly.WeatherCode, i) ?? 0;
            var isDay = IntValueAt(hourly.IsDay, i) == 1;

            hourlyItems.Add(new WeatherHourlyForecastViewModel
            {
                Time = ParseLocalDateTime(hourly.Time[i]),
                TemperatureCelsius = ValueAt(hourly.Temperature2M, i),
                ApparentTemperatureCelsius = ValueAt(hourly.ApparentTemperature, i),
                RelativeHumidityPercent = IntValueAt(hourly.RelativeHumidity2M, i),
                PrecipitationProbabilityPercent = IntValueAt(hourly.PrecipitationProbability, i),
                PrecipitationMm = ValueAt(hourly.Precipitation, i),
                RainMm = ValueAt(hourly.Rain, i),
                ShowersMm = ValueAt(hourly.Showers, i),
                SnowfallCm = ValueAt(hourly.Snowfall, i),
                WeatherCode = weatherCode,
                CloudCoverPercent = ValueAt(hourly.CloudCover, i),
                PressureHpa = ValueAt(hourly.PressureMsl, i),
                VisibilityKm = ToKilometers(ValueAt(hourly.Visibility, i)),
                WindSpeedKph = ValueAt(hourly.WindSpeed10M, i),
                WindDirectionDegrees = IntValueAt(hourly.WindDirection10M, i),
                WindGustsKph = ValueAt(hourly.WindGusts10M, i),
                UvIndex = ValueAt(hourly.UvIndex, i),
                IsDay = isDay,
                Condition = WeatherConditionMapper.Map(weatherCode, isDay)
            });
        }

        model.HourlyForecast = hourlyItems;

        var dailyCount = daily.Time.Count;
        var dailyItems = new List<WeatherDailyForecastViewModel>(dailyCount);

        for (var i = 0; i < dailyCount; i++)
        {
            dailyItems.Add(new WeatherDailyForecastViewModel
            {
                Date = DateOnly.FromDateTime(ParseLocalDateTime(daily.Time[i])),
                Sunrise = ParseLocalDateTime(ValueAt(daily.Sunrise, i)),
                Sunset = ParseLocalDateTime(ValueAt(daily.Sunset, i)),
                MaxTemperatureCelsius = ValueAt(daily.Temperature2MMax, i),
                MinTemperatureCelsius = ValueAt(daily.Temperature2MMin, i),
                ApparentMaxTemperatureCelsius = ValueAt(daily.ApparentTemperatureMax, i),
                ApparentMinTemperatureCelsius = ValueAt(daily.ApparentTemperatureMin, i),
                PrecipitationMm = ValueAt(daily.PrecipitationSum, i),
                RainMm = ValueAt(daily.RainSum, i),
                ShowersMm = ValueAt(daily.ShowersSum, i),
                SnowfallCm = ValueAt(daily.SnowfallSum, i),
                PrecipitationProbabilityMaxPercent = IntValueAt(daily.PrecipitationProbabilityMax, i),
                WindSpeedMaxKph = ValueAt(daily.WindSpeed10MMax, i),
                WindGustsMaxKph = ValueAt(daily.WindGusts10MMax, i),
                DominantWindDirectionDegrees = IntValueAt(daily.WindDirection10MDominant, i),
                DaylightDurationSeconds = ValueAt(daily.DaylightDuration, i),
                SunshineDurationSeconds = ValueAt(daily.SunshineDuration, i),
                UvIndexMax = ValueAt(daily.UvIndexMax, i),
                Condition = WeatherConditionMapper.Map(IntValueAt(daily.WeatherCode, i) ?? 0, true)
            });
        }

        model.DailyForecast = dailyItems;
        model.Highlights = new WeatherHighlightsViewModel
        {
            HumidityPercent = current.RelativeHumidity2M,
            WindSpeedKph = current.WindSpeed10M,
            WindDirectionDegrees = current.WindDirection10M,
            WindDirectionLabel = DescribeWindDirection(current.WindDirection10M),
            UvIndex = current.UvIndex,
            PressureHpa = current.PressureMsl,
            VisibilityKm = ToKilometers(current.Visibility),
            Sunrise = ParseLocalDateTime(ValueAt(daily.Sunrise, 0)),
            Sunset = ParseLocalDateTime(ValueAt(daily.Sunset, 0)),
            CloudCoverPercent = current.CloudCover
        };
        model.Alerts = BuildAlerts(model.Current, model.DailyForecast.FirstOrDefault(), model.Highlights);
    }

    private static DateTime ParseLocalDateTime(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return DateTime.MinValue;
        }

        return DateTime.Parse(value, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AssumeLocal);
    }

    private static double ValueAt(IReadOnlyList<double?>? values, int index)
    {
        if (values is null || index < 0 || index >= values.Count)
        {
            return 0;
        }

        return values[index] ?? 0;
    }

    private static string? ValueAt(IReadOnlyList<string>? values, int index)
    {
        if (values is null || index < 0 || index >= values.Count)
        {
            return null;
        }

        return values[index];
    }

    private static int? IntValueAt(IReadOnlyList<int?>? values, int index)
    {
        if (values is null || index < 0 || index >= values.Count)
        {
            return null;
        }

        return values[index];
    }

    private static double? ToKilometers(double? meters)
    {
        return meters.HasValue ? Math.Round(meters.Value / 1000d, 1) : null;
    }

    private static string DescribeWindDirection(int degrees)
    {
        if (degrees < 0)
        {
            return "Calm";
        }

        var directions = new[]
        {
            "N", "NNE", "NE", "ENE", "E", "ESE", "SE", "SSE",
            "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW"
        };

        var index = (int)Math.Round(degrees / 22.5, MidpointRounding.AwayFromZero) % 16;
        return directions[index];
    }

    private static IReadOnlyList<string> BuildAlerts(
        WeatherCurrentViewModel current,
        WeatherDailyForecastViewModel? today,
        WeatherHighlightsViewModel? highlights)
    {
        var alerts = new List<string>();

        if ((current.UvIndex ?? 0) >= 6)
        {
            alerts.Add("Strong UV conditions are expected around midday. Plan for shade and sun protection.");
        }

        if ((today?.PrecipitationProbabilityMaxPercent ?? 0) >= 60)
        {
            alerts.Add("Rain odds are elevated today, so keeping an umbrella nearby is a smart call.");
        }

        if (current.WindGustsKph >= 35)
        {
            alerts.Add("Wind gusts are picking up and could affect outdoor comfort later in the day.");
        }

        if ((highlights?.VisibilityKm ?? 10) <= 3)
        {
            alerts.Add("Visibility is reduced right now. Travel with extra caution if you are driving.");
        }

        if (alerts.Count == 0)
        {
            alerts.Add("Conditions look stable for now, with no major weather disruptions expected.");
        }

        return alerts;
    }
}
