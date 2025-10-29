using NewsAPI.Models;

namespace NewsAPI.Services
{
    public interface INewsService
    {
        public Task<IReadOnlyList<NewsItem>> Search(NewsQueryFilter filter);
    }
}
