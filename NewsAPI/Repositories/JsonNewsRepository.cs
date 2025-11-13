using NewsAPI.Models;
using System.Text.Json;

namespace NewsAPI.Repositories
{
    public class JsonNewsRepository : INewsRepository
    {
        private readonly string _filePath;

        public JsonNewsRepository(IWebHostEnvironment env)
        {
            _filePath = Path.Combine(env.ContentRootPath, "App_Data", "news.json");
        }

        public async Task<IReadOnlyList<NewsItem>> GetAll()
        {
            if (!File.Exists(_filePath))
            {
                return Array.Empty<NewsItem>();
            }

            var json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<NewsItem>>(json);
        }


    }
}
