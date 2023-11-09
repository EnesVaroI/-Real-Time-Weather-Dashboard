using ClimaDash.Models;
using CsvHelper;
using System.Xml.Linq;

namespace ClimaDash.Services.Implementations
{
    public class CityService : ICityService
    {
        private SortedSet<CityData> _cities;

        private IWebHostEnvironment WebHostEnvironment { get; }

        private string _filePath
        {
            get => Path.Combine(WebHostEnvironment.WebRootPath, "data", "cities_list.csv");
        }

        public CityService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;

            if (!File.Exists(_filePath))
            {
                throw new FileNotFoundException("City list not found.");
            }

            _cities = ReadFromCSVAsync().Result;
        }

        async Task<SortedSet<CityData>> ReadFromCSVAsync()
        {
            using (var reader = new StreamReader(_filePath))
            using (var csv = new CsvReader(reader, true))
            {
                return new SortedSet<CityData>(csv.GetRecords<CityData>());
            }
        }

        public List<CityData> GetCities(string searchText, int limit)
        {
            return _cities
            .Where(city => city.Name.StartsWith(searchText, StringComparison.OrdinalIgnoreCase))
            .Take(limit)
            .ToList();
        }

        public void AddCityIfNotExists(CityData city)
        {
            if (_cities.Any(c => c.Name == city.Name))
            {
                return;
            }

            _cities.Add(city);

            using (var writer = new StreamWriter(_filePath, true))
            using (var csv = new CsvWriter(writer, true))
            {
                csv.WriteRecord(city);
                writer.WriteLine();
            }
        }
    }
}