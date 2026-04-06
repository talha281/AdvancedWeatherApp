using AdvancedWeatherForecast.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedWeatherForecast.Web.Controllers;

public sealed class WeatherController : Controller
{
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? city, CancellationToken cancellationToken)
    {
        var model = await _weatherService.GetDashboardAsync(city, cancellationToken);
        return View(model);
    }
}
