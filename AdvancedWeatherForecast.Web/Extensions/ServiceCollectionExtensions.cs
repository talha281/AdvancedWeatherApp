using AdvancedWeatherForecast.Web.Options;
using AdvancedWeatherForecast.Web.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdvancedWeatherForecast.Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWeatherForecastBackend(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<OpenMeteoOptions>(configuration.GetSection(OpenMeteoOptions.SectionName));

        var options = configuration.GetSection(OpenMeteoOptions.SectionName).Get<OpenMeteoOptions>() ?? new OpenMeteoOptions();

        services.AddMemoryCache();

        services.AddHttpClient(OpenMeteoClientNames.Geocoding, client =>
        {
            client.BaseAddress = new Uri(options.GeocodingBaseUrl, UriKind.Absolute);
            client.DefaultRequestHeaders.UserAgent.ParseAdd("AdvancedWeatherForecast.Web/1.0");
        });

        services.AddHttpClient(OpenMeteoClientNames.Forecast, client =>
        {
            client.BaseAddress = new Uri(options.ForecastBaseUrl, UriKind.Absolute);
            client.DefaultRequestHeaders.UserAgent.ParseAdd("AdvancedWeatherForecast.Web/1.0");
        });

        services.AddScoped<IOpenMeteoClient, OpenMeteoClient>();
        services.AddScoped<IWeatherService, WeatherService>();

        return services;
    }
}
