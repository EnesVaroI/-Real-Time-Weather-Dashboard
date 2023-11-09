namespace ClimaDash.Services.WeatherServices
{
    public interface IAirPollutionService
    {
        //Task<dynamic> GetAirPollutionAsync(string cityName);

        Task<dynamic> GetAirPollutionAsync(double latitude, double longitude);
    }
}
