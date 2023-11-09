using ClimaDash.Services;
using CsvHelper.Configuration.Attributes;

namespace ClimaDash.Models
{
    public record CityData : IComparable<CityData>
    {
        [Name("name")]
        public string Name { get; set; }
        [Name("lon")]
        public double Longitude { get; set; }
        [Name("lat")]
        public double Latitude { get; set; }
        [Name("country")]
        public string Country { get; set; }
        public override string ToString()
        {
            return Name + ", " + Country;
        }
        public int CompareTo(CityData other)
        {
            if (other == null)
            {
                return 1;
            }

            return ToString().CompareTo(other.ToString());
        }
    }
}