namespace ClimaDash.Services
{
    public interface ICityPictureService
    {
        Task<string?> GetCityPictureAsync(string cityName);
    }
}