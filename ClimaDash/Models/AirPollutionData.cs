namespace ClimaDash.Models
{
    public class AirPollutionData
    {
        public List<Coordinate> Coord { get; set; }
        public List<AirQualityMeasurement> List { get; set; }
    }

    public class Coordinate
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
    }

    public class AirQualityMeasurement
    {
        public long Dt { get; set; }
        public AirQualityMain Main { get; set; }
        public AirQualityComponents Components { get; set; }
    }

    public class AirQualityMain
    {
        public int Aqi { get; set; }
    }

    public class AirQualityComponents
    {
        public double CO { get; set; }
        public double NO { get; set; }
        public double NO2 { get; set; }
        public double O3 { get; set; }
        public double SO2 { get; set; }
        public double PM2_5 { get; set; }
        public double PM10 { get; set; }
        public double NH3 { get; set; }
    }
}