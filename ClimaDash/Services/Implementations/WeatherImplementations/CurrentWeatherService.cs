using ClimaDash.Services.WeatherServices;

namespace ClimaDash.Services.Implementations.WeatherImplementations
{
    public class CurrentWeatherService : WeatherService, ICurrentWeatherService
    {

        private readonly string _apiUrl;

        public CurrentWeatherService(IUserSettingsService userSettingsService) : base(userSettingsService)
        {
            _apiUrl = "https://api.openweathermap.org/data/2.5/weather";
        }

        public async Task<dynamic> GetCurrentWeatherAsync(string cityName)
        {
            return await GetWeatherDataAsync<dynamic>($"{_apiUrl}?q={cityName}");
        }

        public async Task<dynamic> GetCurrentWeatherAsync(double latitude, double longitude)
        {
            return await GetWeatherDataAsync<dynamic>($"{_apiUrl}?lat={latitude}&lon={longitude}");
        }

        protected override async Task<T> GetWeatherDataAsync<T>(string endpoint)
        {
            return await base.GetWeatherDataAsync<T>(endpoint);
        }
    }
}
