using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using NewsAPI.Configuration;

namespace NewsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Configuration
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            // My services
            builder.Services.AddProjectServices(builder);

            var app = builder.Build();

            app.UseCors("AllowVue");

            // HealthCheck
            app.MapHealthChecks("/health/live", new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains("liveness")
            });

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
