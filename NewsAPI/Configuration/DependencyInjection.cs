using Microsoft.Extensions.Diagnostics.HealthChecks;
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

            services.AddHealthChecks()
                // Liveness: проверка самого приложения
                .AddCheck("self", () => HealthCheckResult.Healthy("App is alive"), tags: new[] { "liveness" })
                // Readiness: проверка Redis
                .AddRedis(builder.Configuration.GetConnectionString("Redis"), name: "redis", tags: new[] { "readiness" });

            if (builder.Configuration.GetValue<bool>("UseRedis"))
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = builder.Configuration.GetConnectionString("Redis");
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
