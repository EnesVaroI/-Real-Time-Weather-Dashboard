using CsvHelper;

namespace ClimaDash.Services
{
    public interface ICityPictureCache
    {
        bool TryGetCachedImage(string query, out List<string> results);

        void AddToCache(string query, List<string> results);
    }
}
