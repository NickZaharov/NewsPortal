using NewsAPI.Caching;
using NewsAPI.Repositories;
using NewsAPI.Services;

namespace NewsAPI.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddScoped<INewsService, NewsService>();

            services.AddSingleton<INewsRepository, JsonNewsRepository>();

            if (builder.Configuration.GetValue<bool>("UseRedis"))
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = "redis:6379";
                    options.InstanceName = "Cache";
                });

                services.AddSingleton<ICacheService, RedisCacheService>();
            }
            else
            {
                services.AddSingleton<ICacheService, NoOpCacheService>();
            }

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
