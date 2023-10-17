using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClimaDash
{
    public class WeatherService
    {
        private static string _apiKey;
            //= Environment.GetEnvironmentVariable("OpenWeatherMapApiKey");
            

        private static readonly string _apiUrl = "https://api.openweathermap.org/data/2.5/weather";

        private static readonly IConfiguration _configuration;

        public WeatherService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static async Task<dynamic> GetWeatherData(string cityName)
        {
            _apiKey = _configuration["AppSettings:OpenWeatherMapApiKey"];

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{_apiUrl}?q={cityName}&appid={_apiKey}&units=metric");
                    
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        dynamic data = JsonSerializer.Deserialize<dynamic>(json);
                        return data;
                        //return await response.Content.ReadFromJsonAsync<dynamic>();
                    }
                    else
                    {
                        throw new Exception("API request failed with status code: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            //var response2 = await _httpClient.GetFromJsonAsync<dynamic>($"{_apiUrl}?q={cityName}&appid={_apiKey}&units=metric");
            //return response2;
        }
    }
}
