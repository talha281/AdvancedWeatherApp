using AdvancedWeatherForecast.Web.Models.OpenMeteo;

namespace AdvancedWeatherForecast.Web.Services;

public interface IOpenMeteoClient
{
    Task<IReadOnlyList<OpenMeteoLocationDto>> SearchLocationsAsync(string city, CancellationToken cancellationToken);

    Task<OpenMeteoForecastResponse?> GetForecastAsync(OpenMeteoLocationDto location, CancellationToken cancellationToken);
}
