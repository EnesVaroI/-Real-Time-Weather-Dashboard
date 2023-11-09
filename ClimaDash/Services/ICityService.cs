using ClimaDash.Models;

namespace ClimaDash.Services
{
    public interface ICityService
    {
        List<CityData> GetCities(string searchText, int limit = 10);

        void AddCityIfNotExists(CityData city);
    }
}