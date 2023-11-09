namespace ClimaDash.Services.WeatherServices
{
    public interface IWeatherMapService
    {
        Task<byte[]> GetWeatherMapTileAsync(string layer, int zoomLevel, int x, int y);
    }
}
