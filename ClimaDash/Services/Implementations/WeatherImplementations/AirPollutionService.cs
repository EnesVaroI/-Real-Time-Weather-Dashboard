using ClimaDash.Services.WeatherServices;

namespace ClimaDash.Services.Implementations.WeatherImplementations
{
    public class AirPollutionService : WeatherService, IAirPollutionService
    {
        private readonly string _apiUrl;
        
        public AirPollutionService(IUserSettingsService userSettingsService) : base(userSettingsService)
        {
            _apiUrl = "http://api.openweathermap.org/data/2.5/air_pollution";
        }

        //public async Task<dynamic> GetAirPollutionAsync(string cityName)
        //{
        //    return await GetWeatherDataAsync<dynamic>($"{_apiUrl}?q={cityName}");
        //}

        public async Task<dynamic> GetAirPollutionAsync(double latitude, double longitude)
        {
            return await GetWeatherDataAsync<dynamic>($"{_apiUrl}?lat={latitude}&lon={longitude}");
        }

        protected override async Task<T> GetWeatherDataAsync<T>(string endpoint)
        {
            return await base.GetWeatherDataAsync<T>(endpoint);
        }

        public int CalculateAQI(double so2, double no2, double pm10, double pm25, double o3, double co)
        {
            double[] pollutants = { so2, no2, pm10, pm25, o3, co };
            double[] aqiRanges = { 0, 50, 100, 200, 300, 400, 500 };
            double[] breakpoints = { 0, 20, 40, 80, 380, 800, 1600 };

            double maxAQI = 0;

            for (int i = 0; i < pollutants.Length; i++)
            {
                double ratio = (pollutants[i] - breakpoints[i]) / (breakpoints[i + 1] - breakpoints[i]);
                double AQI = (aqiRanges[i + 1] - aqiRanges[i]) * ratio + aqiRanges[i];
                maxAQI = Math.Max(maxAQI, AQI);
            }

            return (int)Math.Round(maxAQI);
        }
    }
}