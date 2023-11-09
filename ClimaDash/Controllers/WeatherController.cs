using ClimaDash.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClimaDash.Controllers
{
    [Route("api/weather")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        //private readonly IWeatherService _weatherService;

        //public WeatherController(IWeatherService weatherService)
        //{
        //    _weatherService = weatherService;
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetWeatherData(string city)
        //{
        //    try
        //    {
        //        //WeatherData weatherData
        //        var weatherData = await _weatherService.GetWeatherAsync(city);
        //        return Ok(weatherData);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //-------------------------------------------------------------------------------------//

        //[HttpGet]
        //public async Task<IActionResult> GetWeatherData(double lat, double lon)
        //{
        //    try
        //    {
        //        //WeatherData weatherData
        //        var weatherData = await _weatherService.GetWeatherDataAsync(lat, lon);
        //        return Ok(weatherData);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //private readonly IHubContext<WeatherHub> _hubContext;

        //public WeatherController(IHubContext<WeatherHub> hubContext)
        //{
        //    _hubContext = hubContext;
        //}

        //public async Task<string> GetWeatherData(string cityName)
        //{
        //    var weatherData = await WeatherService.GetWeatherData(cityName);

        //    var weatherDataJson = JsonSerializer.Serialize(weatherData);
        //    return weatherDataJson;
        //}
    }
}
