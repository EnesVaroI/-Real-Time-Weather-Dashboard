using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClimaDash
{
    public class WeatherController : Controller
    {
        private readonly IHubContext<WeatherHub> _hubContext;

        public WeatherController(IHubContext<WeatherHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task<string> GetWeatherData(string cityName)
        {
            var weatherData = await WeatherService.GetWeatherData(cityName);

            var weatherDataJson = JsonSerializer.Serialize(weatherData);
            return weatherDataJson;
        }
    }
}
