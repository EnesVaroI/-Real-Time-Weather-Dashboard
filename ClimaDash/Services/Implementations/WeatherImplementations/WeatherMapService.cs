using ClimaDash.Services.WeatherServices;

namespace ClimaDash.Services.Implementations.WeatherImplementations
{
    public class WeatherMapService : WeatherService, IWeatherMapService
    {
        private readonly string _apiUrl;

        public WeatherMapService(IUserSettingsService userSettingsService) : base(userSettingsService)
        {
            _apiUrl = "https://tile.openweathermap.org/map";
        }

        public async Task<byte[]> GetWeatherMapTileAsync(string layer, int zoomLevel, int x, int y)
        {
            return await GetWeatherDataAsync<byte[]>($"{_apiUrl}/{layer}/{zoomLevel}/{x}/{y}.png");
        }

        protected override async Task<T> GetWeatherDataAsync<T>(string endpoint)
        {
            return await base.GetWeatherDataAsync<T>(endpoint);
        }
    }
}