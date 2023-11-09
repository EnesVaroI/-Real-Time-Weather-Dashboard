namespace ClimaDash.Services.WeatherServices
{
    public interface IWeatherForecastService
    {
        Task<dynamic> GetWeatherForecastAsync(string cityName);

        Task<dynamic> GetWeatherForecastAsync(double latitude, double longitude);
    }
}
