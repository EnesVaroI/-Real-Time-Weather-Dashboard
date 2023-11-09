using ClimaDash.Models;

namespace ClimaDash.Services.Implementations.WeatherImplementations
{
    public class GeocodingService : WeatherService, ICityService
    {
        private SortedSet<CityData> _cities;

        private readonly string _apiUrl;

        private readonly ICountryCodeService _countryCodeService;

        public GeocodingService(IUserSettingsService userSettingsService, ICountryCodeService countryCodeService) : base(userSettingsService)
        {
            _cities = new();

            _apiUrl = "http://api.openweathermap.org/geo/1.0/direct";

            _countryCodeService = countryCodeService
                ?? throw new ArgumentNullException(nameof(countryCodeService));
        }

        public async Task<dynamic> GetGeocodingAsync(string cityName, int limit)
        {
            limit = (limit < 1) ? 1 : (limit > 5) ? 5 : limit;

            return await GetWeatherDataAsync<dynamic>($"{_apiUrl}?q={cityName}&limit={limit}");
        }

        protected override async Task<T> GetWeatherDataAsync<T>(string endpoint)
        {
            return await base.GetWeatherDataAsync<T>(endpoint);
        }

        public async Task<List<CityData>> GetCitiesAsync(string cityName, int limit)
        {
            _cities.Clear();

            dynamic json = await GetGeocodingAsync(cityName, limit);

            foreach (int i in Enumerable.Range(0, limit))
            {
                var name = json[i].GetProperty("name").GetString();
                var longitude = json[i].GetProperty("lon").GetDouble();
                var latitude = json[i].GetProperty("lat").GetDouble();
                var country = await _countryCodeService.GetCountryByCode(json[i].GetProperty("country").GetString());

                _cities.Add(new CityData()
                {
                    Name = name,
                    Longitude = longitude,
                    Latitude = latitude,
                    Country = country
                });
            }

            return _cities.ToList();
        }

        public List<CityData> GetCities(string cityName, int limit)
        {
            return GetCitiesAsync(cityName, limit).Result;
        }

        public void AddCityIfNotExists(CityData city)
        {
            throw new NotImplementedException();
        }
    }
}