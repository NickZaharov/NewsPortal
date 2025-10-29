using NewsAPI.Models;

namespace NewsAPI.Repositories
{
    public interface INewsRepository
    {
        public Task<IReadOnlyList<NewsItem>> GetAll();
    }
}   
