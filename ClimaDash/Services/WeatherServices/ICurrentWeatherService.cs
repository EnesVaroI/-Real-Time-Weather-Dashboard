namespace ClimaDash.Services.WeatherServices
{
    public interface ICurrentWeatherService
    {
        Task<dynamic> GetCurrentWeatherAsync(string cityName);

        Task<dynamic> GetCurrentWeatherAsync(double latitude, double longitude);
    }
}
