namespace ClimaDash.Services
{
    public interface IUserSettingsService
    {
        string TemperatureUnit { get; set; }
        string Language { get; set; }
    }
}
