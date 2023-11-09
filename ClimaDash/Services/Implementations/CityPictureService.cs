using PexelsDotNetSDK.Api;

namespace ClimaDash.Services.Implementations
{
    public class CityPictureService : ICityPictureService
    {
        private readonly PexelsClient _pexelsClient;

        private readonly ICityPictureCache _cache;

        public CityPictureService(string apiKey, ICityPictureCache cache)
        {
            _pexelsClient = new PexelsClient(apiKey);
            _cache = cache;
        }

        public async Task<string?> GetCityPictureAsync(string cityName)
        {
            List<string> images;

            if (_cache.TryGetCachedImage(cityName, out var cachedImages))
            {
                images = cachedImages;
            }
            else
            {
                var results = await _pexelsClient.SearchPhotosAsync(cityName);
                images = results.photos.Select(p => p.source.original).ToList();

                _cache.AddToCache(cityName, images);
            }

            if (images != null && images.Count > 0)
            {
                var index = new Random().Next(images.Count);
                var picture = images[index];

                return picture;
            }

            return null;
        }
    }
}