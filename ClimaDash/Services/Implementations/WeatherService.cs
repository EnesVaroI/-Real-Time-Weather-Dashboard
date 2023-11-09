using System.Text.Json;

namespace ClimaDash.Services.Implementations
{
    public class WeatherService : IWeatherService
    {
        private readonly string _apiKey;

        private readonly IUserSettingsService _userSettingsService;

        public WeatherService(IUserSettingsService userSettingsService)
        {
            _apiKey = Environment.GetEnvironmentVariable("OPENWEATHER_API_KEY");

            _userSettingsService = userSettingsService
                ?? throw new ArgumentNullException(nameof(userSettingsService));
        }

        protected virtual async Task<T> GetWeatherDataAsync<T>(string endpoint) where T : class
        {
            T? data = null;

            HttpContent content;

            if (typeof(T) == typeof(byte[]))
            {
                content = await HttpRequestAsync($"{endpoint}?appid={_apiKey}");
                data = await content.ReadAsByteArrayAsync() as T;
            }
            else
            {
                content = await HttpRequestAsync($"{endpoint}&appid={_apiKey}&units={_userSettingsService.TemperatureUnit}&lang={_userSettingsService.Language}");
                string json = await content.ReadAsStringAsync();
                data = JsonSerializer.Deserialize<T>(json);
            }

            return data;
        }

        private async Task<HttpContent> HttpRequestAsync(string endpoint)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(endpoint);

                    if (response.IsSuccessStatusCode)
                    {
                        return response.Content;
                    }
                    else
                    {
                        throw new Exception("API request failed with status code: " + response.StatusCode);
                    }
                }
                catch (HttpRequestException ex)
                {
                    throw ex;
                }
            }
        }
    }
}