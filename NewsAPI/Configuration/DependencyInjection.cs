using NewsAPI.Caching;
using NewsAPI.Repositories;
using NewsAPI.Services;

namespace NewsAPI.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services)
        {
            services.AddScoped<INewsService, NewsService>();

            services.AddScoped<ICacheService, RedisCacheService>();

            services.AddSingleton<INewsRepository, JsonNewsRepository>();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "redis:6379";
                options.InstanceName = "Cache";
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowVue", policy =>
                    policy.WithOrigins("http://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
            });

            return services;
        }
    }
}
