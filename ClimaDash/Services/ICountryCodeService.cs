namespace ClimaDash.Services
{
    public interface ICountryCodeService
    {
        Task<string> GetCountryByCode(string code);
    }
}
