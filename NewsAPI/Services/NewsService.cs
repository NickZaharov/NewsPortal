using NewsAPI.Caching;
using NewsAPI.Models;
using NewsAPI.Repositories;

namespace NewsAPI.Services
{
    public class NewsService : INewsService
    {   
        private readonly INewsRepository _repository;
        private readonly ICacheService _cache;

        public NewsService(INewsRepository repository, ICacheService cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public async Task<IReadOnlyList<NewsItem>> Search(NewsQueryFilter filter)
        {

            var allNews = await _cache.GetAsync<IReadOnlyList<NewsItem>>(CacheKeys.News);

            if (allNews == null)
            {
                Console.WriteLine("кэш не был найден");
                allNews = await _repository.GetAll();

                if (allNews != null && allNews.Count != 0)
                    await _cache.SetAsync(CacheKeys.News, allNews, TimeSpan.FromHours(1));
            }

            return allNews.ApplyFilter(filter);
        }   
    }
}
