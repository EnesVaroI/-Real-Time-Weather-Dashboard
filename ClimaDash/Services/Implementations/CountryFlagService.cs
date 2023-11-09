using HtmlAgilityPack;

namespace ClimaDash.Services.Implementations
{
    public class CountryFlagService : ICountryFlagService
    {
        private readonly string _url;

        private List<string> _flags;

        public CountryFlagService()
        {
            _url = "https://en.wikipedia.org/wiki/List_of_sovereign_states";

            _flags = new();
        }

        public async Task<string?> GetFlagForCountryAsync(string countryName)
        {
            if (_flags.Count == 0)
            {
                await ScrapeCountryFlagsAsync();
            }
            
            var formattedCountryName = countryName.Replace(" ", "_");
            
            var flagUrl = _flags.FirstOrDefault(flag => flag.Contains(formattedCountryName));
            
            return flagUrl;
        }

        private async Task ScrapeCountryFlagsAsync()
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(_url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(response);

            _flags = htmlDocument.DocumentNode
                .Descendants("table")
                .Where(table => table.HasClass("wikitable"))
                .SelectMany(table => table.Descendants("img")
                    .Where(img => img.GetAttributeValue("src", "")
                        .StartsWith("//upload.wikimedia.org/wikipedia/commons/")))
                .Select(img => img.GetAttributeValue("src", ""))
                .ToList();
        }
    }
}