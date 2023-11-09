using static ClimaDash.Models.SupportedLanguages;

namespace ClimaDash.Services.Implementations
{
    public class UserSettingsService : IUserSettingsService
    {
        private string _temperatureUnit;
        private string _language;

        public string TemperatureUnit
        {
            get { return _temperatureUnit; }
            set
            {
                if (new[] { "metric", "imperial", "standard" }.Contains(value))
                {
                    _temperatureUnit = value;
                }
                else
                {
                    throw new ArgumentException("Invalid temperature unit.");
                }
            }
        }

        public string Language
        {
            get { return _language; }
            set
            {
                if (Languages.Any(l => l.Key == value))
                {
                    _language = value;
                }
                else
                {
                    throw new ArgumentException("Invalid language code.");
                }
            }
        }

        public UserSettingsService()
        {
            TemperatureUnit = "metric";
            Language = "en";
        }
    }
}