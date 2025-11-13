using NewsAPI.Caching;
using NewsAPI.Models;
using NewsAPI.Repositories;

namespace NewsAPI.Services
{
    public class NewsService : INewsService
    {   
        private readonly INewsRepository _repository;
        private readonly ICacheService _cache;
        private readonly ILogger<NewsService> _logger;

        public NewsService(INewsRepository repository, ICacheService cache, ILogger<NewsService> logger)
        {
            _repository = repository;
            _cache = cache;
            _logger = logger;
        }

        public async Task<IReadOnlyList<NewsItem>> Search(NewsQueryFilter filter)   
        {
            try
            {
                var allNews = await _cache.GetAsync<IReadOnlyList<NewsItem>>(CacheKeys.News);

                if (allNews == null)
                {
                    allNews = await _repository.GetAll();

                    if (allNews != null && allNews.Count != 0)
                        await _cache.SetAsync(CacheKeys.News, allNews, TimeSpan.FromHours(1));
                }

                return (allNews ?? Array.Empty<NewsItem>()).ApplyFilter(filter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении новостей из репозитория");
                return Array.Empty<NewsItem>();
            }
        }   
    }
}
