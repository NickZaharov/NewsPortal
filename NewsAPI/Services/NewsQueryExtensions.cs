using NewsAPI.Models;
using System;

namespace NewsAPI.Services
{
    public static class NewsQueryExtensions
    {
        public static IReadOnlyList<NewsItem> ApplyFilter(this IEnumerable<NewsItem> source, NewsQueryFilter filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                source = source.Where(n =>
                    n.Title.Contains(filter.Search, StringComparison.OrdinalIgnoreCase) ||
                    n.Content.Contains(filter.Search, StringComparison.OrdinalIgnoreCase));
            }

            return source.ToList();
        }
    }
}
