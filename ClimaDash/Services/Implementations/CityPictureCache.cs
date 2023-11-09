namespace ClimaDash.Services.Implementations
{
    public class CityPictureCache : ICityPictureCache
    {
        private Dictionary<string, List<string>> cache = new Dictionary<string, List<string>>();

        public bool TryGetCachedImage(string query, out List<string> results)
        {
            if (cache.TryGetValue(query, out results))
            {
                return true;
            }

            return false;
        }

        public void AddToCache(string query, List<string> results)
        {
            cache[query] = results;
        }
    }
}