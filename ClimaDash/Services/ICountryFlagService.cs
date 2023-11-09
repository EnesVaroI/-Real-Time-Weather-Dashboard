namespace ClimaDash.Services
{
    public interface ICountryFlagService
    {
        Task<string?> GetFlagForCountryAsync(string countryName);
    }
}
