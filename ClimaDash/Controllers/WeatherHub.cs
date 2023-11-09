using ClimaDash.Views.Components;
using Microsoft.AspNetCore.SignalR;

namespace ClimaDash.Controllers
{
    public class WeatherHub : Hub
    {
        public async Task SendWeatherUpdate(Weather weatherData)
        {
            await Clients.All.SendAsync("ReceiveWeatherUpdate", weatherData);
        }
    }
}