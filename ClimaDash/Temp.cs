using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

public class Temp
{
    public static async Task GetData()
    {
        // Replace with your OpenWeatherMap API key and desired city
        string apiKey = "2b71d84695ee6bd55a61a5c20029c677";
        string city = "London";

        // Construct the API URL
        string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}";

        var doc = XDocument.Load("index.xhml");
        var images = doc.Descendants("img");


        using (HttpClient client = new HttpClient())
        {
            try
            {
                // Make the API request and get the response
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Parse the JSON response (you would typically use a JSON library)
                    // For simplicity, we'll just print the raw JSON to the console
                    Console.WriteLine("Weather Data:");
                    Console.WriteLine(responseBody);
                }
                else
                {
                    Console.WriteLine("API request failed with status code: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }

    public static async Task<string> Data()
    {
        // Replace with your OpenWeatherMap API key and desired city
        string apiKey = "2b71d84695ee6bd55a61a5c20029c677";
        string city = "London";

        // Construct the API URL
        string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}";

        using (HttpClient client = new HttpClient())
        {
            try
            {
                // Make the API request and get the response
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Parse the JSON response (you would typically use a JSON library)
                    // For simplicity, we'll just print the raw JSON to the console
                    return "Weather Data:\n" + responseBody;
                }
                else
                {
                    return "API request failed with status code: " + response.StatusCode;
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}