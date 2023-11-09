using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Web;

namespace ClimaDash.Services.Implementations
{
    public class CountryCodeService : ICountryCodeService
    {
        private readonly string _url;

        private List<KeyValuePair<string, string>> _dataList;

        public CountryCodeService()
        {
            _url = "https://en.wikipedia.org/wiki/List_of_ISO_3166_country_codes";

            _dataList = new();
        }

        public async Task<string> GetCountryByCode(string code)
        {
            if (_dataList.Count == 0)
            {
                await ScrapeCountryCodesAsync();
            }

            var countryName = _dataList.Where(x => x.Key == code).FirstOrDefault();

            if (countryName.Value != null)
            {
                return countryName.Value;
            }

            throw new ArgumentException("Invalid country code");
        }

        private async Task ScrapeCountryCodesAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string html = await client.GetStringAsync(_url);

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);

                var table = doc.DocumentNode.SelectSingleNode("//table[@class='wikitable sortable']");
                
                if (table != null)
                {
                    foreach (var row in table.SelectNodes(".//tr"))
                    {
                        var cells = row.SelectNodes(".//td");

                        if (cells != null && cells.Count >= 2)
                        {
                            string countryCode = cells[3].InnerText.Trim();
                            string countryName = CleanseCountryName(cells[0].InnerText);

                            countryName = EditCountryName(countryCode, countryName);

                            _dataList.Add(new KeyValuePair<string, string>(countryCode, countryName));
                        }
                    }

                    AddCountry();
                }
            }
        }

        string CleanseCountryName(string input)
        {
            var decoded = HttpUtility.HtmlDecode(input);
            var cleaned = Regex.Replace(decoded, @"\s*\[.*?\]\s*|\s*\(.*?\)\s*", string.Empty).Trim();

            return cleaned;
        }

        string EditCountryName(string code, string name) => (code, name) switch
        {
            ("BQ", "Bonaire Sint Eustatius Saba") => "Bonaire, Sint Eustatius and Saba",
            ("BN", "Brunei Darussalam") => "Brunei",
            ("CC", "CocosIslands") => "Cocos Islands",
            ("CD", "Congo") => "Democratic Republic of the Congo",
            ("CG", "Congo") => "Republic of the Congo",
            ("CI", "Côte d'Ivoire") => "Ivory Coast",
            ("KP", "Korea") => "North Korea",
            ("KR", "Korea") => "South Korea",
            ("LA", "Lao People's Democratic Republic") => "Laos",
            ("NL", "Netherlands, Kingdom of the") => "Netherlands",
            ("PS", "Palestine, State of") => "Palestine",
            ("RU", "Russian Federation") => "Russia",
            ("SH", "Saint Helena Ascension Island Tristan da Cunha") => "Saint Helena, Ascension and Tristan da Cunha",
            ("SJ", "Svalbard Jan Mayen") => "Svalbard and Jan Mayen",
            ("SY", "Syrian Arab Republic") => "Syria",
            ("TZ", "Tanzania, the United Republic of") => "Tanzania",
            ("TR", "Türkiye") => "Turkey",
            ("GB", "United Kingdom of Great Britain and Northern Ireland") => "United Kingdom",
            ("US", "United States of America") => "United States",
            ("VN", "Viet Nam") => "Vietnam",
            ("VG", "Virgin Islands") => "British Virgin Islands",
            ("VI", "Virgin Islands") => "U.S. Virgin Islands",
            _ => name
        };

        void AddCountry()
        {
            _dataList.Add(new KeyValuePair<string, string>("GL", "Greenland"));
            _dataList.Add(new KeyValuePair<string, string>("XK", "Kosovo"));
        }
    }
}