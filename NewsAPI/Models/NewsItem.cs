namespace NewsAPI.Models
{
    public class NewsItem
    {
        public string Title { get; init; } = string.Empty;
        public DateTime Date { get; init; } = DateTime.UtcNow;
        public string Content { get; init; } = string.Empty;
        public string ImageUrl { get; init; } = string.Empty;
    }

}
