using Microsoft.AspNetCore.SignalR;

namespace ClimaDash
{
    public class WeatherHub : Hub
    {
        public async Task SendWeatherUpdate(WeatherData weatherData)
        {
            await Clients.All.SendAsync("ReceiveWeatherUpdate", weatherData);
        }
    }
}