using ClimaDash.Services.WeatherServices;

namespace ClimaDash.Services.Implementations.WeatherImplementations
{
    public class WeatherForecastService : WeatherService, IWeatherForecastService
    {
        private readonly string _apiUrl;

        public WeatherForecastService(IUserSettingsService userSettingsService) : base(userSettingsService)
        {
            _apiUrl = "https://api.openweathermap.org/data/2.5/forecast";
        }

        public async Task<dynamic> GetWeatherForecastAsync(string cityName)
        {
            return await GetWeatherDataAsync<dynamic>($"{_apiUrl}?q={cityName}");
        }

        public async Task<dynamic> GetWeatherForecastAsync(double latitude, double longitude)
        {
            return await GetWeatherDataAsync<dynamic>($"{_apiUrl}?lat={latitude}&lon={longitude}");
        }

        protected override async Task<T> GetWeatherDataAsync<T>(string endpoint)
        {
            return await base.GetWeatherDataAsync<T>(endpoint);
        }
    }
}
