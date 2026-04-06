using AdvancedWeatherForecast.Web.Models.ViewModels;

namespace AdvancedWeatherForecast.Web.Services;

public interface IWeatherService
{
    Task<WeatherDashboardViewModel> GetDashboardAsync(string? city, CancellationToken cancellationToken);
}
